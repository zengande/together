export interface IUserState {
    isAuthenticated: boolean;
    id: string | null;
    username: string | null;
    nickname: string | null
}

export interface IState {
    user: IUserState
}