export interface IMenu {
    icon: string;
    text: string;
    link: string;
    children: IMenu[] | null
}