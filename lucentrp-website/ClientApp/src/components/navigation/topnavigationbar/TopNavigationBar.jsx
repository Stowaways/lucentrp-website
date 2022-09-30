import Divider from '../../layout/divider/Divider'

import './topnavigationbar.css'

/**
 * Create a TopNavigationBar.
 * 
 * Each child component of the navigation bar should specify a justification property of "left", "center", or "right".
 * 
 * @param {*} props Properties that will be used when rending the navigation bar.
 * @returns The navigation bar.
 */
const TopNavigationBar = (props) => {
    const children = Array.isArray(props.children) ? props.children : (props.children ? [props.children] : [])

    return (
        <nav id={props.id ? props.id : ""} className={"top-navbar center" + (props.className ? ` ${props.className}` : "")}>
            <Divider>
                <div>
                    {children.filter(child => child.props.justified === "left")}
                </div>
                <div>
                    {children.filter(child => child.props.justified === "center")}
                </div>
                <div>
                    {children.filter(child => child.props.justified === "right")}
                </div>
            </Divider>
        </nav>
    );
}

export default TopNavigationBar;