import { useContext } from 'react';

import { AccountContext } from '../../../context/AccountContext/AccountContext';

import TopNavigationBar from '../../navigation/topnavigationbar/TopNavigationBar';
import TopNavigationButton from '../../navigation/topnavigationbutton/TopNavigationButton';

import HamburgerMenu from "../../navigation/hamburgermenu/HamburgerMenu";
import SlidingMenu from "../../navigation/slidingmenu/SlidingMenu";

import useWindowSize from "../../../hooks/useWindowSize";

/**
 * Generate the shared page navigation bar.
 * 
 * @returns The shared page navigation bar.
 */
const PageNavigationBar = () => {
    const { account } = useContext(AccountContext);
    const { width } = useWindowSize();

    if (width <= process.env.REACT_APP_HAMBURGER_START_WIDTH)
        return (
            <TopNavigationBar>
                <HamburgerMenu justified="left" Content={SlidingMenu} contentProps={{side: "left"}}>
                    <TopNavigationButton justified="left" href="/" content="Home" />
                    {account.isLoggedIn && <TopNavigationButton justified="left" href="/dashboard" content="Dashboard" />}
                    {!account.isLoggedIn && <TopNavigationButton justified="right" href="/signup" content="Signup" />}
                    {!account.isLoggedIn && <TopNavigationButton justified="right" href="/login" content="Login" />}
                    {account.isLoggedIn && <TopNavigationButton justified="right" href="/logout" context="Logout" />}
                </HamburgerMenu>
                <TopNavigationButton justified="center" href="/" content="LucentRP" className="logo-text" />
            </TopNavigationBar>
        );
    else
        return (
            <TopNavigationBar>
                <TopNavigationButton justified="left" href="/" content="Home" />
                {account.isLoggedIn && <TopNavigationButton justified="left" href="/dashboard" content="Dashboard" />}
                
                <TopNavigationButton justified="center" href="/" content="LucetRP" className="logo-text"/>

                {!account.isLoggedIn && <TopNavigationButton justified="right" href="/signup" content="Signup" />}
                {!account.isLoggedIn && <TopNavigationButton justified="right" href="/login" content="Login" />}
                {account.isLoggedIn && <TopNavigationButton justified="right" href="/logout" context="Logout" />}
            </TopNavigationBar>
        );
}

export default PageNavigationBar;