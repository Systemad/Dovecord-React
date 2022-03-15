import React from "react";
import {useNavigate} from "react-router-dom";
import {ButtonDiscover, SearchIcon} from "./styles";

export interface Props {
    discoverActive?: boolean;
    to?: string
}

export const DiscoverButton: React.FC<Props> = ({
    discoverActive,
    to
}) => {

    const navigate = useNavigate();
    const navigateDiscover = () => {
        if(to)
            navigate(to)
    }
    return (
        <ButtonDiscover onClick={() => navigateDiscover()}>
            <SearchIcon />
        </ButtonDiscover>
    )
};