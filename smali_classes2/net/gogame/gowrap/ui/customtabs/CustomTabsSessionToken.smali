.class public Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;
.super Ljava/lang/Object;
.source "CustomTabsSessionToken.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken$DummyCallback;
    }
.end annotation


# static fields
.field private static final TAG:Ljava/lang/String; = "CustomTabsSessionToken"


# instance fields
.field private final mCallback:Lnet/gogame/gowrap/ui/customtabs/CustomTabsCallback;

.field private final mCallbackBinder:Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;)V
    .locals 0

    .line 82
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 83
    iput-object p1, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;->mCallbackBinder:Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;

    .line 84
    new-instance p1, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken$1;

    invoke-direct {p1, p0}, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken$1;-><init>(Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;)V

    iput-object p1, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;->mCallback:Lnet/gogame/gowrap/ui/customtabs/CustomTabsCallback;

    return-void
.end method

.method static synthetic access$000(Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;)Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;
    .locals 0

    .line 30
    iget-object p0, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;->mCallbackBinder:Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;

    return-object p0
.end method

.method public static createDummySessionTokenForTesting()Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;
    .locals 2

    .line 79
    new-instance v0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;

    new-instance v1, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken$DummyCallback;

    invoke-direct {v1}, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken$DummyCallback;-><init>()V

    invoke-direct {v0, v1}, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;-><init>(Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;)V

    return-object v0
.end method

.method public static getSessionTokenFromIntent(Landroid/content/Intent;)Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;
    .locals 1

    .line 66
    invoke-virtual {p0}, Landroid/content/Intent;->getExtras()Landroid/os/Bundle;

    move-result-object p0

    const-string v0, "android.support.customtabs.extra.SESSION"

    .line 67
    invoke-virtual {p0, v0}, Landroid/os/Bundle;->getBinder(Ljava/lang/String;)Landroid/os/IBinder;

    move-result-object p0

    if-nez p0, :cond_0

    const/4 p0, 0x0

    return-object p0

    .line 69
    :cond_0
    new-instance v0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;

    invoke-static {p0}, Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback$Stub;->asInterface(Landroid/os/IBinder;)Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;

    move-result-object p0

    invoke-direct {v0, p0}, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;-><init>(Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;)V

    return-object v0
.end method


# virtual methods
.method public equals(Ljava/lang/Object;)Z
    .locals 1

    .line 146
    instance-of v0, p1, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;

    if-nez v0, :cond_0

    const/4 p1, 0x0

    return p1

    .line 147
    :cond_0
    check-cast p1, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;

    .line 148
    invoke-virtual {p1}, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;->getCallbackBinder()Landroid/os/IBinder;

    move-result-object p1

    iget-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;->mCallbackBinder:Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;

    invoke-interface {v0}, Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;->asBinder()Landroid/os/IBinder;

    move-result-object v0

    invoke-virtual {p1, v0}, Ljava/lang/Object;->equals(Ljava/lang/Object;)Z

    move-result p1

    return p1
.end method

.method public getCallback()Lnet/gogame/gowrap/ui/customtabs/CustomTabsCallback;
    .locals 1

    .line 156
    iget-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;->mCallback:Lnet/gogame/gowrap/ui/customtabs/CustomTabsCallback;

    return-object v0
.end method

.method getCallbackBinder()Landroid/os/IBinder;
    .locals 1

    .line 136
    iget-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;->mCallbackBinder:Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;

    invoke-interface {v0}, Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;->asBinder()Landroid/os/IBinder;

    move-result-object v0

    return-object v0
.end method

.method public hashCode()I
    .locals 1

    .line 141
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;->getCallbackBinder()Landroid/os/IBinder;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/Object;->hashCode()I

    move-result v0

    return v0
.end method

.method public isAssociatedWith(Lnet/gogame/gowrap/ui/customtabs/CustomTabsSession;)Z
    .locals 1

    .line 163
    invoke-virtual {p1}, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSession;->getBinder()Landroid/os/IBinder;

    move-result-object p1

    iget-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;->mCallbackBinder:Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;

    invoke-virtual {p1, v0}, Ljava/lang/Object;->equals(Ljava/lang/Object;)Z

    move-result p1

    return p1
.end method
