/**
 * Create an error icon.
 * 
 * @param {object} props Properties that will be used to render the error icon. 
 * An alt property must be specified.
 * 
 * @returns The error icon.
 */
const ErrorIcon = ({ id, className, onClick, alt }) => {
    return (
        <img
            id={id ? id : ""}
            className={className ? className : ""}
            src="/icons/error.png"
            alt={alt}
            onClick={onClick}
        />
    );
}

export default ErrorIcon;
