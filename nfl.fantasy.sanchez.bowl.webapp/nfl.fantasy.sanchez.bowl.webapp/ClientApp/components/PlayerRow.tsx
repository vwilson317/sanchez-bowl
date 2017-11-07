import * as React from 'react';
import { PlayerDetail } from './Home';

interface PlayerRowProp {
    playerDetail: PlayerDetail
}

export class PlayerRow extends React.Component<PlayerRowProp, {}> {
    public render() {
        let p = this.props.playerDetail;
        return <div key={p.name}>
            <div>{p.name} {p.position} {p.score}</div>
        </div>
    }
}