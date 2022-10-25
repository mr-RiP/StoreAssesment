import { NextPage } from 'next'
import { useRouter } from 'next/router'
import StoreContainer from '../../components/StoreContainer'

const Details: NextPage = () => {
	const router = useRouter()
	const { id } = router.query

	return (
		<StoreContainer>
			<h2>Детали: {id}</h2>
		</StoreContainer>
	)
}

export default Details