import CloseIcon from '../../shared/icons/CloseIcon';
import './slidingmenu.css';

/**
 * Create a SlidingMenu.
 * 
 * The side property can be used to set the side of the page that the menu
 * will open from.
 * 
 * A menuIsOpen boolean property must be passed in along with a setMenuIsOpen 
 * proprety.
 * 
 * @param {*} props Properties that will be used to render the sliding menu.
 * @returns The sliding menu.
 */
const SlidingMenu = ({ id, className, side, children, menuIsOpen, setMenuIsOpen }) => {
    return (
        <nav
            id={id ? id : ""}
            className={"sliding-menu" + (menuIsOpen ? " sliding-menu-open" : "") + (side === "right" ? " sliding-menu-right" : " sliding-menu-left") + (className ? ` ${className}` : "")}
        >
            <div className="close-wrapper">
                <CloseIcon className="small-icon" alt="Close button" onClick={() => setMenuIsOpen(false)} />
            </div>
            <div className="sliding-menu-inner-container">
                {children}
            </div>
        </nav>
    );
}

export default SlidingMenu;
