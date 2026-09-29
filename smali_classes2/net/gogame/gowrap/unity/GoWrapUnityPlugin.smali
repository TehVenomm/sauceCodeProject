.class public Lnet/gogame/gowrap/unity/GoWrapUnityPlugin;
.super Ljava/lang/Object;
.source "GoWrapUnityPlugin.java"


# static fields
.field private static final INSTANCE:Lnet/gogame/gowrap/unity/GoWrapUnityPlugin;

.field private static final TAG:Ljava/lang/String; = "goWrap-Unity"


# instance fields
.field private final delegate:Lnet/gogame/gowrap/sdk/GoWrapDelegateV2;

.field private gameObjectName:Ljava/lang/String;


# direct methods
.method static constructor <clinit>()V
    .locals 1

    .line 15
    new-instance v0, Lnet/gogame/gowrap/unity/GoWrapUnityPlugin;

    invoke-direct {v0}, Lnet/gogame/gowrap/unity/GoWrapUnityPlugin;-><init>()V

    sput-object v0, Lnet/gogame/gowrap/unity/GoWrapUnityPlugin;->INSTANCE:Lnet/gogame/gowrap/unity/GoWrapUnityPlugin;

    return-void
.end method

.method public constructor <init>()V
    .locals 1

    .line 12
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const-string v0, "goWrap"

    .line 16
    iput-object v0, p0, Lnet/gogame/gowrap/unity/GoWrapUnityPlugin;->gameObjectName:Ljava/lang/String;

    .line 17
    new-instance v0, Lnet/gogame/gowrap/unity/GoWrapUnityPlugin$1;

    invoke-direct {v0, p0}, Lnet/gogame/gowrap/unity/GoWrapUnityPlugin$1;-><init>(Lnet/gogame/gowrap/unity/GoWrapUnityPlugin;)V

    iput-object v0, p0, Lnet/gogame/gowrap/unity/GoWrapUnityPlugin;->delegate:Lnet/gogame/gowrap/sdk/GoWrapDelegateV2;

    return-void
.end method

.method static synthetic access$000(Lnet/gogame/gowrap/unity/GoWrapUnityPlugin;)Ljava/lang/String;
    .locals 0

    .line 12
    iget-object p0, p0, Lnet/gogame/gowrap/unity/GoWrapUnityPlugin;->gameObjectName:Ljava/lang/String;

    return-object p0
.end method

.method public static initialize(Ljava/lang/String;)V
    .locals 1

    .line 91
    sget-object v0, Lnet/gogame/gowrap/unity/GoWrapUnityPlugin;->INSTANCE:Lnet/gogame/gowrap/unity/GoWrapUnityPlugin;

    iput-object p0, v0, Lnet/gogame/gowrap/unity/GoWrapUnityPlugin;->gameObjectName:Ljava/lang/String;

    .line 92
    sget-object p0, Lnet/gogame/gowrap/unity/GoWrapUnityPlugin;->INSTANCE:Lnet/gogame/gowrap/unity/GoWrapUnityPlugin;

    iget-object p0, p0, Lnet/gogame/gowrap/unity/GoWrapUnityPlugin;->delegate:Lnet/gogame/gowrap/sdk/GoWrapDelegateV2;

    invoke-static {p0}, Lnet/gogame/gowrap/sdk/GoWrap;->setDelegate(Lnet/gogame/gowrap/sdk/GoWrapDelegate;)V

    return-void
.end method
