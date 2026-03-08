#import <AVFoundation/AVFoundation.h>
#import <UIKit/UIKit.h>

static void ApplyPlaybackAudioSession(void)
{
    AVAudioSession *session = [AVAudioSession sharedInstance];
    NSError *error = nil;

    [session setCategory:AVAudioSessionCategoryPlayback
                    mode:AVAudioSessionModeDefault
                 options:0
                   error:&error];
    if (error) {
        NSLog(@"setCategory error: %@", error);
        error = nil;
    }

    [session setActive:YES error:&error];
    if (error) {
        NSLog(@"setActive error: %@", error);
    }
}

@interface UnityAudioSessionObserver : NSObject
@end

@implementation UnityAudioSessionObserver

- (instancetype)init
{
    self = [super init];
    if (self) {
        NSNotificationCenter *nc = [NSNotificationCenter defaultCenter];

        [nc addObserver:self
               selector:@selector(onDidBecomeActive:)
                   name:UIApplicationDidBecomeActiveNotification
                 object:nil];

        [nc addObserver:self
               selector:@selector(onWillEnterForeground:)
                   name:UIApplicationWillEnterForegroundNotification
                 object:nil];

        [nc addObserver:self
               selector:@selector(onAudioInterruption:)
                   name:AVAudioSessionInterruptionNotification
                 object:[AVAudioSession sharedInstance]];
    }
    return self;
}

- (void)onDidBecomeActive:(NSNotification *)notification
{
    ApplyPlaybackAudioSession();
}

- (void)onWillEnterForeground:(NSNotification *)notification
{
    ApplyPlaybackAudioSession();
}

- (void)onAudioInterruption:(NSNotification *)notification
{
    NSDictionary *info = notification.userInfo;
    AVAudioSessionInterruptionType type =
        [info[AVAudioSessionInterruptionTypeKey] unsignedIntegerValue];

    if (type == AVAudioSessionInterruptionTypeEnded) {
        ApplyPlaybackAudioSession();
    }
}

@end

static UnityAudioSessionObserver *gObserver = nil;

void InitPlaybackAudioSession(void)
{
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        gObserver = [[UnityAudioSessionObserver alloc] init];
        ApplyPlaybackAudioSession();
    });
}

void SetAudioSessionPlayback(void)
{
    ApplyPlaybackAudioSession();
}