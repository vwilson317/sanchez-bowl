import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';
import { PlayerRow } from './PlayerRow';
import * as queryStr from 'query-string';


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

    public static playerOnClick(event: any): void {
        let something = event;
        //    this.setState({ selectedPlayer: playerDetail });
        console.log("something was clicked");
    }

    public render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : Home.renderTeams(this.state.teams);

        return contents;
    }

    private static renderTeams(teams: Team[]) {
        return <div id="content">
            {teams.map((t, idx) =>
                <div id={idx + "-team"} key={t.name} className="team-container">
                    <h2>{t.name}</h2>
                    <div className="starters-container">
                        {Home.renderPlayerDetails(t.roster.starters)}
                    </div>
                    <div className="bench-container">
                        {Home.renderPlayerDetails(t.roster.bench)}
                    </div>
                    <h4>{t.totalScore}</h4>
                </div>
            )}
        </div>
    }

    private static renderPlayerDetails(playerDetails: PlayerDetail[]) {
        let startersList = <div onClick={e => this.playerOnClick(e)}>
            {
                playerDetails.map((p) =>
                    <PlayerRow playerDetail={p} />
                )
            }
        </div>

        return startersList;
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
} 