import React from 'react';
import { useGame } from "../../hooks/use-game";
import { useAuth } from "../../hooks/use-auth";
import SVG from 'react-inlinesvg';
import handshake from "../../../svg/iconmonstr-handshake-6.svg";

export default function () {
    const auth = useAuth();
    const game = useGame();
    return (<div className="game-step game-home-step">
        <SVG src={handshake} />
        <p>Hoş geldin, {auth.userName}.</p>
        <button className="btn btn-secondary" type="button" onClick={()=>game.start()}>Yarışmaya Başla</button>
    </div>);
}