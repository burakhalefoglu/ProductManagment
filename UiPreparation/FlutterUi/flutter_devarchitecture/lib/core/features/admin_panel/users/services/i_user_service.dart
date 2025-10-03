import '/core/utilities/results.dart';

import '../../../../services/i_service.dart';

abstract class IUserService implements IService {
  Future<IDataResult<Map<String, dynamic>>> getUserByUserIdWithToken();
}
