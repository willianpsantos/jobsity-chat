import { Home } from "./components/Home";
import { Login }  from "./components/Login";

const AppRoutes = [
    {
        index: true, 
        element: <Home />
    },

    {
        path: '/login',
        element: <Login />
    }
];

export default AppRoutes;
