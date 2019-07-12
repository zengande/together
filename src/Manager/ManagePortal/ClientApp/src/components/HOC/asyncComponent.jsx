import LoadingSpinner from '../LoadingSpinner';
import Loadable from 'react-loadable';

export default asyncComponent = (loader, loading = <LoadingSpinner />) => {
    return Loadable({
        loader: loader,
        loading: () => loading
    });
}