import Head from 'next/head'
import { useRouter } from 'next/router'
import { FunctionComponent } from 'react'
import Container from 'react-bootstrap/Container'
import Nav from 'react-bootstrap/Nav'
import Navbar from 'react-bootstrap/Navbar'
import StoreModeButton from './StoreModeButton'

const StoreHeader: FunctionComponent = () => {
	const { pathname } = useRouter();
	
	return (
		<>
			<Head>
				<title>{pathname}</title>
				<meta name="viewport" content="width=device-width, initial-scale=1" />
			</Head>
			<Navbar bg="light" expand="lg">
				<Container>
					<Navbar.Brand href="/">Продуктовый магазин</Navbar.Brand>
					<Navbar.Toggle aria-controls="basic-navbar-nav" />
					<Navbar.Collapse id="basic-navbar-nav">
						<Nav className="me-auto" activeKey={pathname}>
							<Nav.Link href="/">Список товаров</Nav.Link>
							<Nav.Link href="/cart">Корзина</Nav.Link>
						</Nav>
					</Navbar.Collapse>
					<Navbar.Collapse className="justify-content-end">
						<Navbar.Text>
							<StoreModeButton />
						</Navbar.Text>
					</Navbar.Collapse>
				</Container>
			</Navbar>
			</>
		)
}

export default StoreHeader