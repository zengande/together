import config from '../../config';
import { request } from 'umi'

class ActivityService {
    public fetchCatalogs(): Promise<any> {
        return request(`${config.ApiBaseAddress}/catalogs`)
    }
}

export default new ActivityService();