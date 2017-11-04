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
            : Home.renderTeam(this.state.teams[0].roster.starters);
        //Home.render2(this.state.teams[0].roster.starters)

        return contents;
    }

    private static renderTeam(playerDetails: PlayerDetails[]) {
        let startersList = <div>
            {
                playerDetails.map((p) =>
                    <div key={p.name}>
                        <div>{p.name} {p.position} {p.score}</div>
                    </div>
                )
            };
        </div>;

        return startersList;
    }

    private static render2(playerDetails: PlayerDetails[]) {
        return <table className='table'>
            <thead>
                <tr>
                    <th>Date</th>
                    <th>Temp. (C)</th>
                    <th>Temp. (F)</th>
                    <th>Summary</th>
                </tr>
            </thead>
            <tbody>
                {playerDetails.map(p =>
                    <tr key={p.name}>
                        <td>{p.name}</td>
                        <td>{p.score}</td>
                        <td>{p.position}</td>
                        <td>{p.positionType}</td>
                    </tr>
                )}
            </tbody>
        </table>;
    }
}

interface Team {
    name: string;
    totalScore: number;
    roster: Roster;
}

interface Roster {
    //    public IList<PlayerDetails> Starters { get; }
    //public IList < PlayerDetails > Bench { get; }

    //public int Count => Starters.Count + Bench.Count;
    starters: PlayerDetails[];
    bench: PlayerDetails[];
    count: number;
}

interface PlayerDetails {
    //    public string Name { get; set; }
    //public string Position { get; set; }
    //public double Score { get; set; }

    //public Positions PositionType => (Positions) Enum.Parse(typeof (Positions), Position ?.Split('-')[0].Trim());

    //public bool IsStarter { get; set; }
    name: string;
    position: string;
    score: number;
    positionType: number;
    isStarter: boolean;
} 