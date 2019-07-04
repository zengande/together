export class StorageService {
    private storage: any;

    constructor() {
        this.storage = localStorage;
    }

    public retrieve(key: string, defValue: any = null) {
        let item = this.storage.getItem(key);

        if (item && item !== '') {
            return JSON.parse(item);
        }
        return defValue;
    }

    public store(key: string, value: any) {
        this.storage.setItem(key, JSON.stringify(value));
    }
}

export const storeService = new StorageService();