#import <AVFoundation/AVFoundation.h>

@interface AudioSessionFix : NSObject
@end

@implementation AudioSessionFix

+ (void)load
{
    // Runs when the app loads (early).
    AVAudioSession *session = [AVAudioSession sharedInstance];
    NSError *error = nil;

    // Playback = plays even when the Silent switch is ON.
    // You can add options like mixing if you want.
    if (![session setCategory:AVAudioSessionCategoryPlayback
                  withOptions:0
                        error:&error]) {
        NSLog(@"AudioSession setCategory error: %@", error);
    }

    error = nil;
    if (![session setActive:YES error:&error]) {
        NSLog(@"AudioSession setActive error: %@", error);
    }
}

@end
