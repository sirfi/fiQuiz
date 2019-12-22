import React from 'react';
import SVG from 'react-inlinesvg';
import checkMark from "../../../svg/iconmonstr-check-mark-14.svg";

export default function () {
    return (<div className="game-step game-correct-answer-step">
        <SVG src={checkMark} />
        <p>Soruya doğru cevap verdin. Tebrikler.</p>
    </div>);
}