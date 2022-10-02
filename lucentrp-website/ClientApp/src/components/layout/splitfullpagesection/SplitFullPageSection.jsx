import "./splitfullpagesection.css";

/**
 * Create a SplitFullPageSection.
 * 
 * Each child component must specify the side of the section they will be rendered on
 * through the side property (side="left" or side="right").
 * 
 * @param {*} properties Properties that will be used when rendering the component.
 * @returns The split full page section.
 */
const SplitFullPageSection = ({ id, className, subtractNav, children }) => {
    let childComponents = Array.isArray(children) ? children : [children];

    return (
        <section>
            <section id={id ? id : ""} className={"center " + (subtractNav ? "split-full-page-section-sub-nav" : "split-full-page-section") + (className ? ` ${className}` : "")}>
                <div className="split-full-page-inner-wrapper">
                    <div className="split-full-page-inner-container">
                        {childComponents.filter(child => child.props.side === "left")}
                    </div>
                </div>
                <div className="split-full-page-inner-wrapper">
                    <div className="split-full-page-inner-container">
                        {childComponents.filter(child => child.props.side === "right")}
                    </div>
                </div>
            </section>
        </section>
    );
}

export default SplitFullPageSection;