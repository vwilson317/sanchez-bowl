import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';

interface HomeState {
    teams: Team[];
    loading: boolean;
}

export class Home extends React.Component<RouteComponentProps<{}>, HomeState> {
    constructor() {
        super();
        this.state = { teams: [], loading: true };

        fetch('api/teams/12/week/9')
            .then(response => response.json() as Promise<Team>)
            .then(data => {
                this.setState({ teams: [data], loading: false });
            });
    }

    public render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : Home.renderTeam(this.state.teams[0]);
        return contents;
    }

    private static renderTeam(team: Team) {
        return <div>
                   <p>{team.name}</p>
                   <p>{team.totalScore}</p>
               </div>;
    }
}

interface Team {
    name: string;
    totalScore: number;
}