.class public Lnet/gogame/gowrap/ui/DialogStartMenuManager;
.super Ljava/lang/Object;
.source "DialogStartMenuManager.java"

# interfaces
.implements Lnet/gogame/gowrap/ui/StartMenuManager;


# static fields
.field private static final DEFAULT_ACTIVITY_CLASS:Ljava/lang/Class;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/lang/Class<",
            "+",
            "Landroid/app/Activity;",
            ">;"
        }
    .end annotation
.end field

.field public static final INSTANCE:Lnet/gogame/gowrap/ui/DialogStartMenuManager;


# direct methods
.method static constructor <clinit>()V
    .locals 1

    .line 10
    new-instance v0, Lnet/gogame/gowrap/ui/DialogStartMenuManager;

    invoke-direct {v0}, Lnet/gogame/gowrap/ui/DialogStartMenuManager;-><init>()V

    sput-object v0, Lnet/gogame/gowrap/ui/DialogStartMenuManager;->INSTANCE:Lnet/gogame/gowrap/ui/DialogStartMenuManager;

    .line 11
    const-class v0, Lnet/gogame/gowrap/ui/MainActivity;

    sput-object v0, Lnet/gogame/gowrap/ui/DialogStartMenuManager;->DEFAULT_ACTIVITY_CLASS:Ljava/lang/Class;

    return-void
.end method

.method public constructor <init>()V
    .locals 0

    .line 8
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public hideMenu()V
    .locals 0

    return-void
.end method

.method public showMenu(Landroid/app/Activity;)V
    .locals 2

    .line 15
    sget-object v0, Lnet/gogame/gowrap/GoWrapImpl;->INSTANCE:Lnet/gogame/gowrap/GoWrapImpl;

    invoke-virtual {v0}, Lnet/gogame/gowrap/GoWrapImpl;->getMainActivity()Ljava/lang/Class;

    move-result-object v0

    if-nez v0, :cond_0

    .line 17
    sget-object v0, Lnet/gogame/gowrap/ui/DialogStartMenuManager;->DEFAULT_ACTIVITY_CLASS:Ljava/lang/Class;

    .line 19
    :cond_0
    new-instance v1, Landroid/content/Intent;

    invoke-direct {v1, p1, v0}, Landroid/content/Intent;-><init>(Landroid/content/Context;Ljava/lang/Class;)V

    const/high16 v0, 0x20000

    .line 20
    invoke-virtual {v1, v0}, Landroid/content/Intent;->setFlags(I)Landroid/content/Intent;

    .line 21
    invoke-virtual {p1, v1}, Landroid/app/Activity;->startActivity(Landroid/content/Intent;)V

    return-void
.end method
