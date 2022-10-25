import Head from 'next/head';
import Link from 'next/link';
import { useRouter } from 'next/router';
import { FunctionComponent } from 'react';

const StoreHeader: FunctionComponent = () => {
	const router = useRouter();

	return (
		<>
			<Head>
				<title>{router.pathname}</title>
			</Head>
			<header>
				<ul>
					<h1>{router.pathname}</h1>
					<li>
						<Link href="/">
							<a>Список продуктов</a>
						</Link>
					</li>
					<li>
						<Link href="/cart">
							<a>Корзина</a>
						</Link>
					</li>
					<li>
						<Link href="/edit/1">
							<a>Редактировать 1</a>
						</Link>
					</li>
				</ul>
			</header>
		</>
	)
}

export default StoreHeader