export interface ApiRequestBase {
}
export interface ApiResponseBase {
}

export interface StartRequest extends ApiRequestBase {
}
export interface StartResponse extends ApiResponseBase {
    quizId: number;
    jokers: JokerInfo[];
}
export interface JokerInfo {
    joker: QuizJoker;
    jokerName: string;
    isUsed: boolean;
}

export interface GetQuestionRequest extends ApiRequestBase {
    quizId: number;
    quizQuestionId: number;
}
export interface GetQuestionResponse extends ApiResponseBase {
    quizId: number;
    quizQuestionId: number;
    questionText: string;
    categoryName: string;
    answers: Answer[];
    answerTime: number;
}
export enum AnswerOption {
    A,
    B,
    C,
    D
}
export interface Answer {
    answerOption: AnswerOption;
    answer: string;
    isWrongAnswer: boolean;
}

export interface SendAnswerRequest extends ApiRequestBase {
    quizId: number;
    quizQuestionId: number;
    answerOption?: AnswerOption;
}
export interface SendAnswerResponse extends ApiResponseBase {
    quizId: number;
    nextQuizQuestionId?: number;
    nextQuizQuestionNumber?: number;
    isCorrect: boolean;
    isCompleted: boolean;
}

export enum QuizJoker {
    ChangeQuestion,
    HalfAndHalf,
    DoubleAnswer,
    AdditionalTime
}
export interface UseJokerRequest extends ApiRequestBase {
    quizId: number;
    quizQuestionId: number;
    joker: QuizJoker;
}
export interface UseJokerResponse extends ApiResponseBase {
    quizId: number;
    quizQuestionId: number;
    question: {
        quizId: number;
        quizQuestionId: number;
        questionText: string;
        categoryName: string;
        answers: Answer[];
        answerTime: number;
    };
}

export interface SetTimeoutRequest extends ApiRequestBase {
    quizId: number;
}

export interface SetTimeoutResponse extends ApiResponseBase {

}

export enum UserQuizListType {
    Last,
    Top
}

export interface GetUserQuizListRequest extends ApiRequestBase {
    listType: UserQuizListType;
}

export enum QuizCompletionType {
    Successful,
    WrongAnswer,
    Timeout
}

export interface UserQuiz {
    startedAt: Date;
    completedAt: Date;
    completionType: QuizCompletionType;
    completionTypeText: string;
    correctAnswerCount: number;
}

export interface GetUserQuizListResponse extends ApiResponseBase {
    quizzes: UserQuiz[];
}

export interface ReportQuestionRequest extends ApiRequestBase {
    quizId: number;
    quizQuestionId: number;
}
export interface ReportQuestionResponse extends ApiResponseBase {

}

export interface Service {
    start(): Promise<StartResponse>;
    getQuestion(quizId: number, quizQuestionId: number): Promise<GetQuestionResponse>;
    sendAnswer(quizId: number, quizQuestionId: number, answerOption: AnswerOption): Promise<SendAnswerResponse>;
    useJoker(quizId: number, quizQuestionId: number, joker: QuizJoker): Promise<UseJokerResponse>;
    setTimeout(quizId: number): Promise<SetTimeoutResponse>;
    getUserQuizList(listType: UserQuizListType): Promise<GetUserQuizListResponse>;
    reportQuestion(quizId: number, quizQuestionId: number, description: string): Promise<ReportQuestionResponse>;
}

declare const ServiceStatic: Service;

export default ServiceStatic;

