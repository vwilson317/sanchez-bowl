import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';

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
        //props.
        fetch('api/teams/12/week/9')
            .then(response => response.json() as Promise<Team>)
            .then(data => {
                teams.push(data);
                fetch('api/teams/11/week/9')
                    .then(response => response.json() as Promise<Team>)
                    .then(data => {
                        teams.push(data);
                        this.setState({ teams: teams, loading: false });
                    });
            });

        //let playerRows = document.getElementsByClassName("player-row");
        //for (let currentPlayerRow of playerRows) {
        //    playerRows.item.addEventListener("click", (e:Event) => )
        //}

        //    public handleOnClick(event: any): void {
        //    this.setState({ name: "Charles" });
        //}
    }

    public static playerOnClick(event: any): void {
        let something = event;
        //    this.setState({ selectedPlayer: playerDetail });
        console.log("something was clicked");
    }

    //public getPosition: function(playerDetail: PlayerDetail) {
    //}

    public render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : Home.renderTeams(this.state.teams);//Home.renderPlayerDetails(this.state.teams[0].roster.starters);

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
                    <div key={p.name}>
                        <div>{p.name} {p.position} {p.score}</div>
                    </div>
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
    //    public IList<PlayerDetails> Starters { get; }
    //public IList < PlayerDetails > Bench { get; }

    //public int Count => Starters.Count + Bench.Count;
    starters: PlayerDetail[];
    bench: PlayerDetail[];
    count: number;
}

interface PlayerDetail {
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