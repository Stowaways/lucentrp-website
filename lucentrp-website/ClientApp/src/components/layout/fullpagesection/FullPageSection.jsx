import './fullpagesection.css'

/**
 * Create a section that takes up 1 full page.
 * 
 * Pass true in for the subtractNav property if you want to subtract the height
 * of the navigation bar from the full page section.
 * 
 * @param {*} props Properties that will be used when rendering the full page section.
 * @returns The full page section.
 */
const FullPageSection = (props) => {
    return (
        <section id={props.id ? props.id : ""} className={"center " + (props.subtractNav ? "full-page-section-sub-nav" : "full-page-section") + (props.className ? ` ${props.className}` : "")}>
            <div className="full-page-inner-container">
                {props.children}
            </div>
        </section>
    );
}

export default FullPageSection;
