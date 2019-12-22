import React, { useState, useEffect, useContext, createContext } from "react";
import accountService from "../services/AccountService";

const authContext = createContext();

// Provider component that wraps your app and makes auth object ...
// ... available to any child component that calls useAuth().
export function ProvideAuth({ children }) {
    const auth = useProvideAuth();
    return <authContext.Provider value={auth}>{children}</authContext.Provider>;
}

// Hook for child components to get the auth object ...
// ... and re-render when it changes.
export const useAuth = () => {
    return useContext(authContext);
};

// Provider hook that creates auth object and handles state
function useProvideAuth() {
    const [user, setUser] = useState(null);

    const signin = (email, password) => {
        return accountService.login(email, password)
            .then(response => {
                setUser(response);
                return response;
            });
    };

    const signup = (fullName, email, password) => {
        return accountService.register(fullName, email, password)
            .then(response => {
                setUser(response);
                return response;
            });
    };

    const signout = () => {
        return accountService.logout()
            .then(() => {
                setUser(false);
            });
    };

    const sendPasswordResetEmail = email => {
        return Promise.resolve(true);
    };

    const confirmPasswordReset = (code, password) => {
        return Promise.resolve(true);
    };

    // Subscribe to user on mount
    // Because this sets state in the callback it will cause any ...
    // ... component that utilizes this hook to re-render with the ...
    // ... latest auth object.
    useEffect(() => {
        if (!user)
            accountService.getToken().then(user => {
                if (user) {
                    setUser(user);
                } else {
                    setUser(false);
                }
            });

        // Cleanup subscription on unmount
        return () => { };
    }, []);

    // Return the user object and auth methods
    return {
        user,
        get userName() { return user ? user.fullName : "Misafir"; },
        signin,
        signup,
        signout,
        sendPasswordResetEmail,
        confirmPasswordReset
    };
}