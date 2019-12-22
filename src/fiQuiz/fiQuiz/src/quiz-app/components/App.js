import React, { useState, useEffect, useContext, createContext } from "react";
import { useAuth, ProvideAuth } from "../hooks/use-auth";
import { useGame, ProvideGame } from "../hooks/use-game";
import Steps from "./Steps/index";
import Jokers from "./Jokers";
import { UserQuizListType } from "../services/QuizGameService";


function CurrentStep() {
    const game = useGame();
    const Step = Steps[game.step];
    return (<Step />);
}

function ProvidedApp(props) {
    const auth = useAuth();
    const game = useGame();
    return <div className="game-container">
        <div className="game-header">
            <div className="game-title">
                <div className="game-name">Bilgi Yarışması</div>
                <div className="game-user-name">
                    <strong>Yarışmacı: </strong>
                    {auth.userName}</div>
            </div>
            <div className="game-remaining-time">{game.remainingTime}</div>
        </div>
        <div className="game-body">
            {game.jokers ? <Jokers /> : null}
            <CurrentStep />
        </div>
        <div className="game-footer">
            <a href="javascript:" onClick={() => game.goToHome()} className="btn btn-secondary btn-sm">Başa dön</a>
            <a href="javascript:" onClick={() => game.goToUserQuizList(UserQuizListType.Last)} className="btn btn-secondary btn-sm">Son yarışmalar</a>
            <a href="javascript:" onClick={() => game.goToUserQuizList(UserQuizListType.Top)} className="btn btn-secondary btn-sm">En iyi yarışmalar</a>
        </div>
    </div>;
};

function App(props) {
    return (
        <ProvideAuth>
            <ProvideGame>
                <ProvidedApp />
            </ProvideGame>
        </ProvideAuth>
    );
}

export default App;
