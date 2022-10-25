import { NextPage } from 'next'
import { useRouter } from 'next/router'
import StoreContainer from '../../components/StoreContainer'

const Edit: NextPage = () => {
	const router = useRouter()
	const { id } = router.query

	return (
		<StoreContainer>
			<h2>Редактирование: {id}</h2>
		</StoreContainer>
	)
}

export default Edit
