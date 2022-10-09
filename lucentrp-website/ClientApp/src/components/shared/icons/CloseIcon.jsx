/**
 * Create a close icon.
 * 
 * @param {object} props Properties that will be used to render the close icon.
 * An alt property must be specified. 
 * 
 * @returns The close icon.
 */
const CloseIcon = ({ id, className, onClick, alt }) => {
    return (
        <img
            id={id ? id : ""}
            className={className ? className : ""}
            src="/icons/close.png"
            alt={alt}
            onClick={onClick}
        />
    );
}

export default CloseIcon;
