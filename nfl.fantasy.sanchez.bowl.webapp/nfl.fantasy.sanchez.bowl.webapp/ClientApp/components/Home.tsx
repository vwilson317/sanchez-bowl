import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';
import { PlayerRow } from './PlayerRow';
import * as queryStr from 'query-string';
import * as classNames from 'classnames';


interface IHomeState {
    teamOne: ITeam | null;
    teamTwo: ITeam | null;
    loading: boolean;
    selectedPlayer: IPlayerDetail | null;
}

export class Home extends React.Component<RouteComponentProps<{}>, IHomeState> {
    constructor(props: any) {
        super();
        this.state = { teamOne: null, teamTwo: null, loading: true, selectedPlayer: null };
        var teams = [] as ITeam[];
        let parsedParams = queryStr.parse(props.location.search);
        if (parsedParams.teams !== undefined) {
            let teamIds = parsedParams.teams.split(",");
            fetch('api/teams/' + teamIds[0] + '/week/9')
                .then(response => response.json() as Promise<ITeam>)
                .then(data => {
                    teams.push(data);
                    fetch('api/teams/' + teamIds[1] + '/week/9')
                        .then(response => response.json() as Promise<ITeam>)
                        .then(data => {
                            teams.push(data);
                            this.setState({ teamOne: teams[0], teamTwo: teams[1], loading: false });
                        });
                });
        }
    }

    public playerOnClick(playerDetail: IPlayerDetail, e: Event): void {
        //doesn't resolve this is done
        let playerSelected = this.state.selectedPlayer !== null;
        if (playerSelected) {
            var currentId = this.state.selectedPlayer!.teamId;
            var thisTeam = [this.state.teamOne, this.state.teamTwo]
                .filter(team => team!.id === currentId)[0] as ITeam;
            var roster = thisTeam.roster;

            //we only care about the starts being changes ... this will save if the starters order changes
            var swappedPlayers = this.swapPlayers(roster.starters, playerDetail);
            this.swapPlayers(roster.bench, playerDetail);

            if (thisTeam.id === this.state.teamOne!.id) {
                this.setState({ teamOne: thisTeam as ITeam, selectedPlayer: null });

            } else if (thisTeam.id === this.state.teamTwo!.id) {
                this.setState({ teamTwo: thisTeam as ITeam, selectedPlayer: null });
            }
            if (swappedPlayers) {
                this.save(thisTeam);
            }
        }
        else {
            if (playerDetail.score === 0) {
                this.setState({ selectedPlayer: playerDetail });
            }
        }
    }

    private save(team: ITeam) {
        fetch('api/teams', {
            method: 'post',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(team)
        }).then(response => response.json() as Promise<boolean>)
            .then(data => {
                console.log("Save result: ", data);
            });
    }

    private swapPlayers(playerDetails: IPlayerDetail[], clickedPlayerDetail: IPlayerDetail) {
        var swappedPlayers = false;
        for (let i = 0; i < playerDetails.length; i++) {
            var currentPlayer = playerDetails[i];
            var selectedPlayer = this.state.selectedPlayer as IPlayerDetail
            if (currentPlayer.name === selectedPlayer.name && clickedPlayerDetail.positionType === selectedPlayer.positionType) {
                playerDetails[i] = clickedPlayerDetail;
                swappedPlayers = true;
            }
            else if (currentPlayer.name === clickedPlayerDetail.name && clickedPlayerDetail.positionType === selectedPlayer.positionType) {
                playerDetails[i] = selectedPlayer;
                swappedPlayers = true;
            }
        }
        return swappedPlayers;
    }

    public render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderTeams([this.state.teamOne as ITeam, this.state.teamTwo as ITeam]);

        return contents;
    }

    private renderTeams(teams: ITeam[]) {
        return <div id="content">
            {teams.map((t, idx) =>
                <div id={idx + "-team"} key={t.name} className="team-container">
                    <h2>{t.name}</h2>
                    <div className="starters-container">
                        {this.renderPlayerDetails(t.roster.starters)}
                    </div>
                    <div className="bench-container">
                        {this.renderPlayerDetails(t.roster.bench)}
                    </div>
                    <h4>{t.totalScore}</h4>
                </div>
            )}
        </div>
    }

    private renderPlayerDetails(playerDetails: IPlayerDetail[]) {
        let startersList = <div>
            {
                playerDetails.map((p) =>
                    <div onClick={this.playerOnClick.bind(this, p)} className={this.addClass(p)}>
                        <PlayerRow key={p.name} playerDetail={p} />
                    </div>
                )
            }
        </div>

        return startersList;
    }

    private addClass(playerDetail: IPlayerDetail) {
        let classObj = { selectable: false };
        if (this.state.selectedPlayer !== null) {
            var thisPlayerDetail = this.state.selectedPlayer;
            let isSelectable = thisPlayerDetail!.positionType === playerDetail.positionType
                && thisPlayerDetail!.teamId === playerDetail.teamId && playerDetail.score === 0;
            classObj.selectable = isSelectable;
        }

        return classNames(classObj);
    }
}

interface ITeam {
    id: number;
    name: string;
    totalScore: number;
    roster: IRoster;
}

interface IRoster {
    starters: IPlayerDetail[];
    bench: IPlayerDetail[];
    count: number;
}

export interface IPlayerDetail {
    name: string;
    position: string;
    score: number;
    positionType: number;
    isStarter: boolean;
    teamId: number;
} 