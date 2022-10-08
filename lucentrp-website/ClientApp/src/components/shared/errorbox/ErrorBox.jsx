import "./errorbox.css"

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