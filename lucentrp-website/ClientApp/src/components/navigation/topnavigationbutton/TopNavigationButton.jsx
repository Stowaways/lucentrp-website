import './topnavigationbutton.css'

/**
 * Create a top navigation button.
 * 
 * Use the content attribute to specify content that should be rendered within the button.
 * 
 * @param {*} props Properties that will be used when rendering the button.
 * @returns The top navigation button.
 */
const TopNavigationButton = (props) => {
    return (
        <a
            id={props.id ? props.id : ""}
            className={"top-nav-button center" + (props.className ? ` ${props.className}` : "")}
            href={props.href}
        >
            {props.content}
        </a>
    );
}

export default TopNavigationButton;