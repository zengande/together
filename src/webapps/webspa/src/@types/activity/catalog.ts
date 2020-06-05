export default interface ActivityCatalog {
    id: number;
    name: string;
    children?: ActivityCatalog[]
}