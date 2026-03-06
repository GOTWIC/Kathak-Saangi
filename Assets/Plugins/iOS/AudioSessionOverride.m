#import <AVFoundation/AVFoundation.h>

void SetAudioSessionPlayback()
{
    NSError *error = nil;
    AVAudioSession *session = [AVAudioSession sharedInstance];

    [session setCategory:AVAudioSessionCategoryPlayback error:&error];
    [session setActive:YES error:&error];
}