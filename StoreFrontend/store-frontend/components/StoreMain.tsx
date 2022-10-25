import { FunctionComponent } from 'react'
import styles from '../styles/Home.module.css'

type Props = {
	children?: JSX.Element | JSX.Element[]
}

const StoreMain: FunctionComponent<Props> = ({ children }) => {
	return (
		<main className={styles.main}>
			{children}
		</main>
	)
}

export default StoreMain