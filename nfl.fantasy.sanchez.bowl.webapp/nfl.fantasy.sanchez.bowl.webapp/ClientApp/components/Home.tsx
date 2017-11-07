import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';
import { PlayerRow } from './PlayerRow';
import * as queryStr from 'query-string';
import * as classNames from 'classnames';


interface HomeState {
    teams: Team[];
    loading: boolean;
    selectedPlayer: PlayerDetail | null;
}

export class Home extends React.Component<RouteComponentProps<{}>, HomeState> {
    constructor(props: any) {
        super();
        this.state = { teams: [], loading: true, selectedPlayer: null };
        var teams = [] as Team[];
        let parsedParams = queryStr.parse(props.location.search);
        let teamIds = parsedParams.teams.split(",");
        fetch('api/teams/' + teamIds[0] + '/week/9')
            .then(response => response.json() as Promise<Team>)
            .then(data => {
                teams.push(data);
                fetch('api/teams/' + teamIds[1] + '/week/9')
                    .then(response => response.json() as Promise<Team>)
                    .then(data => {
                        teams.push(data);
                        this.setState({ teams: teams, loading: false });
                    });
            });
    }

    public playerOnClick(playerDetail: PlayerDetail, e: Event): void {
        console.log("something was clicked");

        //doesn't resolve this is done
        let playerSelected = this.state.selectedPlayer !== null;
        if (playerSelected) {
            //var playerRowDomElements = document.getElementsByClassName("player-row");
            //for (let currentDomEle of playerRowDomElements) {
            //    currentDomEle.addClass("selectable");
            //}
        }
        else {
            this.setState({ selectedPlayer: playerDetail });
        }
    }

    public render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderTeams(this.state.teams);

        return contents;
    }

    private renderTeams(teams: Team[]) {
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

    private renderPlayerDetails(playerDetails: PlayerDetail[]) {
        let startersList = <div>
            {
                playerDetails.map((p) =>
                    <div onClick={this.playerOnClick.bind(this, p)} className={this.addClass(p)}>
                        <PlayerRow key={p.name} playerDetail={p}  />
                    </div>
                )
            }
        </div>

        return startersList;
    }

    private addClass(playerDetail: PlayerDetail) {
        let classObj = { selectable: false };
        if (this.state.selectedPlayer !== null) {
            var thisPlayerDetail = this.state.selectedPlayer;
            let isSelectable = thisPlayerDetail.positionType === playerDetail.positionType
            && thisPlayerDetail.teamId === playerDetail.teamId;
            classObj.selectable = isSelectable;
        }

        return classNames(classObj);
    }
}

interface Team {
    name: string;
    totalScore: number;
    roster: Roster;
}

interface Roster {
    starters: PlayerDetail[];
    bench: PlayerDetail[];
    count: number;
}

export interface PlayerDetail {
    name: string;
    position: string;
    score: number;
    positionType: number;
    isStarter: boolean;
    teamId: number;
} 