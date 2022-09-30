import TopNavigationBar from '../navigation/topnavigationbar/TopNavigationBar';
import TopNavigationButton from '../navigation/topnavigationbutton/TopNavigationButton';

/**
 * Generate the shared page navigation bar.
 * 
 * @returns The shared page navigation bar.
 */
const PageNavigationBar = () => {
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