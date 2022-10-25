import { FunctionComponent } from 'react'
import styles from '../styles/Home.module.css'
import StoreFooter from './StoreFooter'
import StoreHeader from './StoreHeader'
import StoreMain from './StoreMain'

type Props = {
	children?: JSX.Element | JSX.Element[]
}

const StoreContainer: FunctionComponent<Props> = ({ children }) => {
	return (
		<div className={styles.container}>
			<StoreHeader />

			<StoreMain>
				{children}
			</StoreMain>
			
			<StoreFooter />
		</div>
	)
}

export default StoreContainer