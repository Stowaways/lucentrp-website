import React, { useState } from 'react'

/**
 * The account context.
 */
export const AccountContext = React.createContext({
    account: {
        isLoggedIn: false,
        id: null,
        accountCreated: null,
        email: null,
        username: null,
        emailVerified: null
    },
    setAccount: () => { }
})

/**
 * The account context provider.
 * 
 * @param {object} props Properties passed in to the component.
 * @returns The component.
 */
export const AccountContextProvider = (props) => {
    const setAccount = (account) => {
        setState({ ...state, account: account })
    }

    const initState = {
        account: {
            isLoggedIn: false,
            id: null,
            accountCreated: null,
            email: null,
            username: null,
            emailVerified: null
        },
        setAccount: setAccount
    }

    const [state, setState] = useState(initState)

    return (
        <AccountContext.Provider value={state}>
            {props.children}
        </AccountContext.Provider>
    )
}
