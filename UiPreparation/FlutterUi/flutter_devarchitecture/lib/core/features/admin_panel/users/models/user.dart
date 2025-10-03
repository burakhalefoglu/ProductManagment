import '/core/models/i_entity.dart';

class User implements IEntity {
  @override
  late int id;
  late int userId;
  String fullName;
  String email;
  String mobilePhones;
  bool status;
  String password;

  String? youtubeProfile;
  String? instagramProfile;
  String? spotifyProfile;
  String? videoOrSoundUrl;

  String? musicStyle;
  String? manager;
  String? studio;
  String? inspirationArtists;
  String biography;

  User({
    this.id = 0,
    this.userId = 0,
    required this.fullName,
    required this.email,
    required this.mobilePhones,
    required this.status,
    this.youtubeProfile,
    this.instagramProfile,
    this.spotifyProfile,
    this.videoOrSoundUrl,
    this.musicStyle,
    this.manager,
    this.studio,
    this.inspirationArtists,
    required this.biography,
    required this.password,
  }) {
    id = userId;
  }

  factory User.fromMap(Map<String, dynamic> map) {
    return User(
      id: map['userId'] ?? 0,
      userId: map['userId'] ?? 0,
      fullName: map['fullName'] ?? '',
      email: map['email'] ?? '',
      mobilePhones: map['mobilePhones'] ?? '',
      status: map['status'] ?? true,
      youtubeProfile: map['youtubeProfile'],
      instagramProfile: map['instagramProfile'],
      spotifyProfile: map['spotifyProfile'],
      videoOrSoundUrl: map['videoOrSoundUrl'],
      musicStyle: map['musicStyle'],
      manager: map['manager'],
      studio: map['studio'],
      inspirationArtists: map['inspirationArtists'],
      biography: map['biography'] ?? '',
      password: map['password'] ?? '',
    );
  }

  Map<String, dynamic> toMap() {
    return {
      'id': userId,
      'userId': userId,
      'fullName': fullName,
      'email': email,
      'mobilePhones': mobilePhones,
      'status': status,
      'youtubeProfile': youtubeProfile,
      'instagramProfile': instagramProfile,
      'spotifyProfile': spotifyProfile,
      'videoOrSoundUrl': videoOrSoundUrl,
      'musicStyle': musicStyle,
      'manager': manager,
      'studio': studio,
      'inspirationArtists': inspirationArtists,
      'biography': biography,
      'password': password
    };
  }
}
