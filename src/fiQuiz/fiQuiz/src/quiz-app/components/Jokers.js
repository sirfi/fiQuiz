import React from 'react';
import { useGame } from "../hooks/use-game";
import SVG from 'react-inlinesvg';
import arrow from "../../svg/iconmonstr-arrow-60.svg";
import time from "../../svg/iconmonstr-time-22.svg";
import copy from "../../svg/iconmonstr-copy-14.svg";
import weather from "../../svg/iconmonstr-weather-112.svg";

const jokerIcons = {
    ChangeQuestion: arrow,
    HalfAndHalf: weather,
    DoubleAnswer: copy,
    AdditionalTime: time
}
export default function () {
    const game = useGame();
    return (<div className={"quiz-jokers" + (game.step !== "showQuestion"?" disabled":"")}>
        {game.jokers.map(x =>
            <a key={x.joker} href="javascript:" className={"btn btn-secondary btn-quiz-joker" + (x.isUsed?" disabled":"")} title={x.jokerName} onClick={() => game.useJoker(x.joker)}>
                <SVG src={jokerIcons[x.joker]} />
                <span className="joker-name">{x.jokerName}</span>
            </a>)}
    </div>);
}