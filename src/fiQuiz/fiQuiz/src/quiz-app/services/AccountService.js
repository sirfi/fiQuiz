import axios from 'axios';

const accountService = window.AccountService = {
    login,
    logout,
    register,
    getToken
}

async function login(email, password) {
    const response = await axios.post('/api/Account/Login',
        {
            email: email,
            password: password
        });
    axios.defaults.headers["Authorization"] = "bearer " + response.token;
    return response.data;
}

async function register(fullName, email, password) {
    const response = await axios.post('/api/Account/Register',
        {
            fullName: fullName,
            email: email,
            password: password
        });
    axios.defaults.headers["Authorization"] = "bearer " + response.token;
    return response.data;
}

async function getToken() {
    if (window.userToken) return Promise.resolve(window.userToken);
    const response = await axios.get('/api/Account/GetToken');
    axios.defaults.headers["Authorization"] = "bearer " + response.token;
    return response.data;
}

async function logout() {
    const response = await axios.get('/api/Account/Logout');
    return response.data;
}

export default accountService;