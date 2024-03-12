import * as React from 'react';
import {useEffect, useState} from 'react';
import {AppBar, Avatar, Box, Container, Menu, MenuItem, Toolbar, Typography} from '@mui/material';
import AdbIcon from '@mui/icons-material/Adb';
import IconButton from '@mui/material/IconButton';
import Tooltip from '@mui/material/Tooltip';


import {useNavigate} from "react-router-dom"
import {useCookies} from "react-cookie";
import {MenuIcon} from "lucide-react";

const pages = [
    {name: 'Accueil', url: '/'},
    {name: 'Mon panier', url: '/myCart'}
];
const backoffice = [
    {name: 'G.Produits', url: '/backoffice/products'},
    {name: 'G.Inventaire', url: '/backoffice/inventory'},
    {name: 'G.Fournisseurs', url: '/backoffice/suppliers'},
    {name: 'Commandes Fournisseurs', url: '/backoffice/suppliersOrders'},
    {name: 'G.Client', url: '/backoffice/customers'}
];


function Navbar() {
    const [anchorElNav, setAnchorElNav] = React.useState(null);
    const [anchorElUser, setAnchorElUser] = React.useState(null);

    const handleOpenNavMenu = (event) => {
        setAnchorElNav(event.currentTarget);
    };
    const handleOpenUserMenu = (event) => {
        setAnchorElUser(event.currentTarget);
    };

    const handleCloseNavMenu = () => {
        setAnchorElNav(null);
    };

    const handleCloseUserMenu = () => {
        setAnchorElUser(null);
    };

    const [isLogged, setIsLogged] = useState(false)
    const [isAdmin, setIsAdmin] = useState(false)

    const navigate = useNavigate();

    const [cookie, setCookie, removeCookie] = useCookies(['bearerToken', 'roles']);


    useEffect(() => {
        if (cookie.bearerToken) {
            setIsLogged(true)
            if (cookie.roles.includes('admin')) setIsAdmin(true)
        } else {
            setIsLogged(false)
        }
    }, [cookie]);

    const handleDisconnect = (event) => {
        event.preventDefault();
        removeCookie('bearerToken')
        removeCookie('roles')
        navigate('/')
        window.location.reload();
    }

    const handleLogin = (event) => {
        event.preventDefault();
        navigate('/login')
    }

    const handleMenuClick = (url) => { // Modifier pour accepter l'URL au lieu du nom de page
        navigate(url); // Naviguer vers l'URL spécifiée
        handleCloseNavMenu(); // Fermer le menu après le clic
    };

    return (
        <AppBar position="static">
            <Container maxWidth="xl">
                <Toolbar disableGutters>
                    <AdbIcon sx={{display: {xs: 'none', md: 'flex'}, mr: 1}}/>
                    <Typography
                        variant="h6"
                        noWrap
                        component="a"
                        href="/"
                        sx={{
                            mr: 2,
                            display: {xs: 'none', md: 'flex'},
                            fontFamily: 'monospace',
                            fontWeight: 700,
                            letterSpacing: '.3rem',
                            color: 'inherit',
                            textDecoration: 'none',
                        }}
                    >
                        La Vignerie
                    </Typography>

                    <Box sx={{flexGrow: 1, display: {xs: 'flex', md: 'none'}}}>
                        <IconButton
                            size="large"
                            aria-label="account of current user"
                            aria-controls="menu-appbar"
                            aria-haspopup="true"
                            onClick={handleOpenNavMenu}
                            color="inherit"
                        >
                            <MenuIcon/>
                        </IconButton>
                        <Menu
                            id="menu-appbar"
                            anchorEl={anchorElNav}
                            anchorOrigin={{
                                vertical: 'bottom',
                                horizontal: 'left',
                            }}
                            keepMounted
                            transformOrigin={{
                                vertical: 'top',
                                horizontal: 'left',
                            }}
                            open={Boolean(anchorElNav)}
                            onClose={handleCloseNavMenu}
                            sx={{
                                display: {xs: 'block', md: 'none'},
                            }}
                        >
                            {pages.map((page) => (
                                <MenuItem key={page.name} onClick={() => handleMenuClick(page.url)}>
                                    <Typography textAlign="center">{page.name}</Typography>
                                </MenuItem>
                            ))}
                            {isAdmin && backoffice.map((page) => (
                                <MenuItem key={page.name} onClick={() => handleMenuClick(page.url)}>
                                    <Typography textAlign="center">{page.name}</Typography>
                                </MenuItem>
                            ))}
                        </Menu>
                    </Box>
                    <AdbIcon sx={{display: {xs: 'flex', md: 'none'}, mr: 1}}/>
                    <Typography
                        variant="h5"
                        noWrap
                        component="a"
                        href="#app-bar-with-responsive-menu"
                        sx={{
                            mr: 2,
                            display: {xs: 'flex', md: 'none'},
                            flexGrow: 1,
                            fontFamily: 'monospace',
                            fontWeight: 700,
                            letterSpacing: '.3rem',
                            color: 'inherit',
                            textDecoration: 'none',
                        }}
                    >
                        La Vignerie
                    </Typography>
                    <Box sx={{flexGrow: 1, display: {xs: 'none', md: 'flex'}}}>
                        {pages.map((page) => (
                            <MenuItem key={page.name} onClick={() => handleMenuClick(page.url)}>
                                <Typography textAlign="center">{page.name}</Typography>
                            </MenuItem>
                        ))}
                        {isAdmin && backoffice.map((page) => (
                            <MenuItem key={page.name} onClick={() => handleMenuClick(page.url)}>
                                <Typography textAlign="center">{page.name}</Typography>
                            </MenuItem>
                        ))}
                    </Box>

                    <Box sx={{flexGrow: 0}}>
                        <Tooltip title="Open settings">
                            <IconButton onClick={handleOpenUserMenu} sx={{p: 0}}>
                                <Avatar alt="Remy Sharp" src="/static/images/avatar/2.jpg"/>
                            </IconButton>
                        </Tooltip>
                        <Menu
                            sx={{mt: '45px'}}
                            id="menu-appbar"
                            anchorEl={anchorElUser}
                            anchorOrigin={{
                                vertical: 'top',
                                horizontal: 'right',
                            }}
                            keepMounted
                            transformOrigin={{
                                vertical: 'top',
                                horizontal: 'right',
                            }}
                            open={Boolean(anchorElUser)}
                            onClose={handleCloseUserMenu}
                        >
                            <MenuItem onClick={handleCloseUserMenu}>
                                {isLogged &&
                                    <Typography textAlign="center" onClick={handleDisconnect}>
                                        Se Déconnecter
                                    </Typography>
                                }
                                {!isLogged &&
                                    <Typography textAlign="center" onClick={handleLogin}>Se Connecter</Typography>

                                }
                            </MenuItem>

                        </Menu>
                    </Box>
                </Toolbar>
            </Container>
        </AppBar>
    );
}

export default Navbar;