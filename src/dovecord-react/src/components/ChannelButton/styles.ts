import styled from "styled-components";
import { Hashtag } from "styled-icons/heroicons-outline";
import { PersonAdd, Settings } from "styled-icons/material";

export const Container = styled.div`
    display: flex;
    align-items: center;
    justify-content: space-between;

    cursor: pointer;

    padding: 5px 3px;
    border-radius: 5px;

    background-color: transparent;

    transition: background-color 0.2s;

    > div {
        display: flex;
        align-items: center;
    }

    > div span {
        margin-left: 5px;

        font-size: 15px;
        color: var(--senary);
    }

    > div svg :not(:first-child) {
        display: none;
    }

    &.active,
    &:hover {
        background-color: var(--quinary);

        > div span {
            color: var(--white);
        }

        > div svg :not(:first-child) {
            display: inline;
        }
    }
`;

export const HashtagIcon = styled(Hashtag)`
    height: 20px;
    width: 20px;

    color: var(--symbol);
`;

export const InviteIcon = styled(PersonAdd)`
    height: 16px;
    width: 16px;

    color: var(--symbol);
    cursor: pointer;

    transition: color 0.2s;

    &:hover {
        color: var(--white);
    }
`;

export const SettingsIcon = styled(Settings)`
    height: 16px;
    width: 16px;

    margin-left: 4px;

    color: var(--symbol);
    cursor: pointer;

    transition: color 0.2s;

    &:hover {
        color: var(--white);
    }
`;
