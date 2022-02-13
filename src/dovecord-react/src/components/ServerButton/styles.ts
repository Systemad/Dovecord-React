import styled from "styled-components";

import { Props } from ".";

export const Button = styled.button<Props>`
    display: flex;
    align-items: center;
    justify-content: center;
    flex-shrink: 0;

    width: 48px;
    height: 48px;
    margin-bottom: 8px;
    border-radius: 50%;

    background-color: ${(props) =>
        props.isHome ? "var(--purple)" : "var(--primary)"};

    position: relative;
    cursor: pointer;

    > img {
        width: 28px;
        height: 28px;
    }

    &::before {
        width: 9px;
        height: 9px;

        position: absolute;
        left: -17px;
        top: calc(50% - 4.5px);

        background-color: var(--white);
        border-radius: 50%;

        content: "";
        display: ${(props) => (props.hasNotifications ? "inline" : "none")};
    }

    &::after {
        background-color: var(--notification);
        width: auto;
        height: 16px;

        padding: 0 4px;

        position: absolute;
        bottom: -4px;
        right: -4px;

        border-radius: 12px;
        border: 4px solid var(--quaternary);
        text-align: center;
        font-size: 13px;
        font-weight: bold;
        color: var(--white);

        content: "${(props) => props.mentions && props.mentions}";
        display: ${(props) =>
            props.mentions && props.mentions > 0 ? "inline" : "none"}
    }

    transition: border-radius 0.2s, background-color 0.2s;

    &:active,
    &:hover {
        border-radius: 16px;
        background-color: ${(props) =>
            props.isHome ? "var(--purple)" : "var(--discord)"};
    }
`;
