import styled from "styled-components";
import { SearchAlt } from "@styled-icons/boxicons-regular/SearchAlt"
import {Props} from "./DsicoverButton";

export const Container = styled.div`
    grid-area: SL;

    display: flex;
    flex-direction: column;
    align-items: center;

    background-color: var(--tertiary);
    padding: 11px 0;

    max-height: 100vh;
    overflow-y: scroll;

    ::-webkit-scrollbar {
        display: none;
    }
`;

export const Separator = styled.div`
    width: 32px;
    border-bottom: 2px solid var(--quaternary);

    margin-bottom: 8px;
`;


export const ButtonDiscover = styled.button<Props>`
    display: flex;
    align-items: center;
    justify-content: center;
    flex-shrink: 0;

    width: 48px;
    height: 48px;
    margin-bottom: 8px;
    border-radius: 25%;

    background-color: var(--primary);

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
        display: none;
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

        content: "5";
        display: none;
    }

    transition: border-radius 0.2s, background-color 0.2s;

    &:active,
    &:hover {
        border-radius: 16px;
        background-color: var(--discord);
    }
`;

export const SearchIcon = styled(SearchAlt)`
    width: 24px;
    height: 24px;
    color: var(--gray);
`;