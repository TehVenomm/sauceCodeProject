.class public Lnet/gogame/gowrap/ui/fab/FabManager;
.super Ljava/lang/Object;
.source "FabManager.java"


# static fields
.field private static final fabMap:Ljava/util/Map;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Map<",
            "Landroid/app/Activity;",
            "Lnet/gogame/gowrap/ui/fab/Fab;",
            ">;"
        }
    .end annotation
.end field


# direct methods
.method static constructor <clinit>()V
    .locals 1

    .line 15
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    sput-object v0, Lnet/gogame/gowrap/ui/fab/FabManager;->fabMap:Ljava/util/Map;

    return-void
.end method

.method public constructor <init>()V
    .locals 0

    .line 13
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static handleTouchEvent(Landroid/app/Activity;Landroid/view/MotionEvent;)Z
    .locals 4

    if-nez p0, :cond_0

    .line 81
    sget-object p0, Lnet/gogame/gowrap/ui/ActivityHelper;->INSTANCE:Lnet/gogame/gowrap/ui/ActivityHelper;

    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/ActivityHelper;->getCurrentActivity()Landroid/app/Activity;

    move-result-object p0

    .line 83
    :cond_0
    sget-object v0, Lnet/gogame/gowrap/ui/fab/FabManager;->fabMap:Ljava/util/Map;

    invoke-interface {v0, p0}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lnet/gogame/gowrap/ui/fab/Fab;

    const/4 v1, 0x1

    if-eqz v0, :cond_1

    .line 85
    invoke-interface {v0, p1}, Lnet/gogame/gowrap/ui/fab/Fab;->handleTouchEvent(Landroid/view/MotionEvent;)Z

    move-result v0

    if-eqz v0, :cond_1

    return v1

    .line 89
    :cond_1
    invoke-virtual {p1}, Landroid/view/MotionEvent;->getActionMasked()I

    move-result v0

    const/4 v2, 0x5

    const/4 v3, 0x0

    if-eq v0, v2, :cond_2

    return v3

    .line 92
    :cond_2
    invoke-virtual {p1}, Landroid/view/MotionEvent;->getActionIndex()I

    move-result p1

    const/4 v0, 0x2

    if-ge p1, v0, :cond_3

    return v3

    .line 95
    :cond_3
    invoke-static {p0}, Lnet/gogame/gowrap/ui/fab/FabManager;->showMenu(Landroid/app/Activity;)V

    return v1
.end method

.method public static hideFab(Landroid/app/Activity;)V
    .locals 1

    .line 57
    sget-object v0, Lnet/gogame/gowrap/ui/fab/FabManager;->fabMap:Ljava/util/Map;

    invoke-interface {v0, p0}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lnet/gogame/gowrap/ui/fab/Fab;

    if-eqz v0, :cond_0

    .line 59
    invoke-interface {v0, p0}, Lnet/gogame/gowrap/ui/fab/Fab;->hide(Landroid/app/Activity;)V

    :cond_0
    return-void
.end method

.method public static onCreate(Landroid/app/Activity;)V
    .locals 2

    .line 22
    sget-object v0, Lnet/gogame/gowrap/ui/fab/FabManager;->fabMap:Ljava/util/Map;

    invoke-interface {v0, p0}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lnet/gogame/gowrap/ui/fab/Fab;

    if-nez v0, :cond_0

    .line 24
    new-instance v0, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;

    const/4 v1, 0x0

    invoke-direct {v0, v1, v1}, Lnet/gogame/gowrap/ui/fab/PopupWindowFab;-><init>(ZZ)V

    .line 25
    new-instance v1, Lnet/gogame/gowrap/ui/fab/FabManager$1;

    invoke-direct {v1, p0}, Lnet/gogame/gowrap/ui/fab/FabManager$1;-><init>(Landroid/app/Activity;)V

    invoke-interface {v0, v1}, Lnet/gogame/gowrap/ui/fab/Fab;->setClickListener(Lnet/gogame/gowrap/ui/fab/Fab$ClickListener;)V

    .line 32
    sget-object v1, Lnet/gogame/gowrap/ui/fab/FabManager;->fabMap:Ljava/util/Map;

    invoke-interface {v1, p0, v0}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_0
    return-void
.end method

.method public static onDestroy(Landroid/app/Activity;)V
    .locals 1

    .line 72
    sget-object v0, Lnet/gogame/gowrap/ui/fab/FabManager;->fabMap:Ljava/util/Map;

    invoke-interface {v0, p0}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lnet/gogame/gowrap/ui/fab/Fab;

    if-eqz v0, :cond_0

    .line 74
    invoke-interface {v0, p0}, Lnet/gogame/gowrap/ui/fab/Fab;->destroy(Landroid/app/Activity;)V

    .line 75
    sget-object v0, Lnet/gogame/gowrap/ui/fab/FabManager;->fabMap:Ljava/util/Map;

    invoke-interface {v0, p0}, Ljava/util/Map;->remove(Ljava/lang/Object;)Ljava/lang/Object;

    :cond_0
    return-void
.end method

.method public static onPause(Landroid/app/Activity;)V
    .locals 0

    .line 68
    invoke-static {p0}, Lnet/gogame/gowrap/ui/fab/FabManager;->hideFab(Landroid/app/Activity;)V

    return-void
.end method

.method public static onResume(Landroid/app/Activity;)V
    .locals 0

    .line 64
    invoke-static {p0}, Lnet/gogame/gowrap/ui/fab/FabManager;->showFab(Landroid/app/Activity;)V

    return-void
.end method

.method public static showFab(Landroid/app/Activity;)V
    .locals 2

    .line 37
    sget-object v0, Lnet/gogame/gowrap/ui/fab/FabManager;->fabMap:Ljava/util/Map;

    invoke-interface {v0, p0}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lnet/gogame/gowrap/ui/fab/Fab;

    if-eqz v0, :cond_0

    .line 40
    invoke-interface {v0, p0}, Lnet/gogame/gowrap/ui/fab/Fab;->show(Landroid/app/Activity;)V

    .line 42
    sget-object v1, Lnet/gogame/gowrap/integrations/core/Wrapper;->INSTANCE:Lnet/gogame/gowrap/integrations/core/Wrapper;

    invoke-virtual {v1}, Lnet/gogame/gowrap/integrations/core/Wrapper;->isServerDown()Z

    move-result v1

    if-eqz v1, :cond_0

    .line 43
    invoke-interface {v0, p0}, Lnet/gogame/gowrap/ui/fab/Fab;->update(Landroid/app/Activity;)V

    :cond_0
    return-void
.end method

.method public static showMenu(Landroid/app/Activity;)V
    .locals 1

    .line 18
    sget-object v0, Lnet/gogame/gowrap/ui/DialogStartMenuManager;->INSTANCE:Lnet/gogame/gowrap/ui/DialogStartMenuManager;

    invoke-virtual {v0, p0}, Lnet/gogame/gowrap/ui/DialogStartMenuManager;->showMenu(Landroid/app/Activity;)V

    return-void
.end method

.method public static update(Landroid/app/Activity;)V
    .locals 1

    .line 49
    sget-object v0, Lnet/gogame/gowrap/ui/fab/FabManager;->fabMap:Ljava/util/Map;

    invoke-interface {v0, p0}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lnet/gogame/gowrap/ui/fab/Fab;

    if-eqz v0, :cond_0

    .line 52
    invoke-interface {v0, p0}, Lnet/gogame/gowrap/ui/fab/Fab;->update(Landroid/app/Activity;)V

    :cond_0
    return-void
.end method
