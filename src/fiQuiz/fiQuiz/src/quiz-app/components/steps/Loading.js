import React, {useState} from 'react';
import SVG from 'react-inlinesvg';
import loading from "../../../svg/iconmonstr-loading-10.svg";

export default function () {
    return (<div className="game-step game-loading-step">
        <SVG src={loading} />
        <p>Lütfen bekleyin.</p>
    </div>);
}