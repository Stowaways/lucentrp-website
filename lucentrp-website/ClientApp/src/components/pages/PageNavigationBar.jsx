import TopNavigationBar from '../navigation/topnavigationbar/TopNavigationBar';
import TopNavigationButton from '../navigation/topnavigationbutton/TopNavigationButton';

import HamburgerMenu from "../navigation/hamburgermenu/HamburgerMenu";
import SlidingMenu from "../navigation/slidingmenu/SlidingMenu";

import useWindowSize from "../../hooks/useWindowSize";

/**
 * Generate the shared page navigation bar.
 * 
 * @returns The shared page navigation bar.
 */
const PageNavigationBar = () => {
    const { width } = useWindowSize();

    if (width <= process.env.REACT_APP_HAMBURGER_START_WIDTH)
        return (
            <TopNavigationBar>
                <HamburgerMenu justified="left" Content={SlidingMenu} contentProps={{side: "left"}}>
                    <TopNavigationButton justified="left" href="/" content="Home" />
                    <TopNavigationButton justified="right" href="/signup" content="Signup" />
                    <TopNavigationButton justified="right" href="/login" content="Login" />
                </HamburgerMenu>
                <TopNavigationButton justified="center" href="/" content="LucentRP" className="logo-text" />
            </TopNavigationBar>
        );
    else
        return (
            <TopNavigationBar>
                <TopNavigationButton justified="left" href="/" content="Home" />

                <TopNavigationButton justified="center" href="/" content="LucetRP" className="logo-text"/>

                <TopNavigationButton justified="right" href="/signup" content="Signup" />
                <TopNavigationButton justified="right" href="/login" content="Login" />
            </TopNavigationBar>
        );
}

export default PageNavigationBar;