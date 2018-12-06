import * as React from 'react';
import './App.css';
import { Route } from 'react-router';
import Home from './pages/Home';
import { Authorize } from './components/Authorized';

const Layout = (props: any) => {
    console.log(props);
    return (<div>{props.children}</div>)
};

const ProtectedHome = Authorize(Home);

const Login =(props:any)=>(<h1>login</h1>);
class App extends React.Component<{}> {
    public render() {
        return (
            <Layout>
                <Route exact={true} path={["/","/home"]} component={ProtectedHome} />
                <Route path="/login" component={Login} />
            </Layout>
        );
    }
}
export default App;