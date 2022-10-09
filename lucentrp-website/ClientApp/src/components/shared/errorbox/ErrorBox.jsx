import "./errorbox.css"

/**
 * Create an error box, that can be used to display error messages.
 * 
 * @param {object} props Properties that will be used to render the error box. 
 * @returns The error box.
 */
const ErrorBox = ({ id, className, children }) => {
    return (
        <div
        id={id ? id : ""}
        className={"error-box center" + (className ? ` ${className}` : "")}
        >
            {children}
        </div>
    );
}

export default ErrorBox;
