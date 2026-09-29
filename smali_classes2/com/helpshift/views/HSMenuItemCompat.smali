.class public Lcom/helpshift/views/HSMenuItemCompat;
.super Ljava/lang/Object;
.source "HSMenuItemCompat.java"


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 16
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static collapseActionView(Landroid/view/MenuItem;)V
    .locals 2

    .line 60
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getApplicationContext()Landroid/content/Context;

    move-result-object v0

    const/16 v1, 0x1a

    invoke-static {v0, v1}, Lcom/helpshift/util/ApplicationUtil;->isSupportLibVersionEqualAndAbove(Landroid/content/Context;I)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 61
    invoke-interface {p0}, Landroid/view/MenuItem;->collapseActionView()Z

    goto :goto_0

    .line 64
    :cond_0
    invoke-static {p0}, Landroidx/core/view/MenuItemCompat;->collapseActionView(Landroid/view/MenuItem;)Z

    :goto_0
    return-void
.end method

.method public static expandActionView(Landroid/view/MenuItem;)V
    .locals 2

    .line 69
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getApplicationContext()Landroid/content/Context;

    move-result-object v0

    const/16 v1, 0x1a

    invoke-static {v0, v1}, Lcom/helpshift/util/ApplicationUtil;->isSupportLibVersionEqualAndAbove(Landroid/content/Context;I)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 70
    invoke-interface {p0}, Landroid/view/MenuItem;->expandActionView()Z

    goto :goto_0

    .line 73
    :cond_0
    invoke-static {p0}, Landroidx/core/view/MenuItemCompat;->expandActionView(Landroid/view/MenuItem;)Z

    :goto_0
    return-void
.end method

.method public static getActionView(Landroid/view/MenuItem;)Landroid/view/View;
    .locals 2

    .line 39
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getApplicationContext()Landroid/content/Context;

    move-result-object v0

    const/16 v1, 0x1a

    invoke-static {v0, v1}, Lcom/helpshift/util/ApplicationUtil;->isSupportLibVersionEqualAndAbove(Landroid/content/Context;I)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 40
    invoke-interface {p0}, Landroid/view/MenuItem;->getActionView()Landroid/view/View;

    move-result-object p0

    goto :goto_0

    .line 43
    :cond_0
    invoke-static {p0}, Landroidx/core/view/MenuItemCompat;->getActionView(Landroid/view/MenuItem;)Landroid/view/View;

    move-result-object p0

    :goto_0
    return-object p0
.end method

.method public static isActionViewExpanded(Landroid/view/MenuItem;)Z
    .locals 2

    .line 50
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getApplicationContext()Landroid/content/Context;

    move-result-object v0

    const/16 v1, 0x1a

    invoke-static {v0, v1}, Lcom/helpshift/util/ApplicationUtil;->isSupportLibVersionEqualAndAbove(Landroid/content/Context;I)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 51
    invoke-interface {p0}, Landroid/view/MenuItem;->isActionViewExpanded()Z

    move-result p0

    goto :goto_0

    .line 54
    :cond_0
    invoke-static {p0}, Landroidx/core/view/MenuItemCompat;->isActionViewExpanded(Landroid/view/MenuItem;)Z

    move-result p0

    :goto_0
    return p0
.end method

.method public static setOnActionExpandListener(Landroid/view/MenuItem;Landroid/view/MenuItem$OnActionExpandListener;)V
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "<T::",
            "Landroid/view/MenuItem$OnActionExpandListener;",
            ":",
            "Landroidx/core/view/MenuItemCompat$OnActionExpandListener;",
            ">(",
            "Landroid/view/MenuItem;",
            "TT;)V"
        }
    .end annotation

    .line 21
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getApplicationContext()Landroid/content/Context;

    move-result-object v0

    const/16 v1, 0x1a

    invoke-static {v0, v1}, Lcom/helpshift/util/ApplicationUtil;->isSupportLibVersionEqualAndAbove(Landroid/content/Context;I)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 23
    :try_start_0
    invoke-interface {p0, p1}, Landroid/view/MenuItem;->setOnActionExpandListener(Landroid/view/MenuItem$OnActionExpandListener;)Landroid/view/MenuItem;
    :try_end_0
    .catch Ljava/lang/UnsupportedOperationException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    const-string v1, "HSMenuItemCompat"

    const-string v2, "Exception thrown while calling MenuItem#setOnActionExpandListener: "

    .line 28
    invoke-static {v1, v2, v0}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    .line 29
    check-cast p1, Landroidx/core/view/MenuItemCompat$OnActionExpandListener;

    invoke-static {p0, p1}, Landroidx/core/view/MenuItemCompat;->setOnActionExpandListener(Landroid/view/MenuItem;Landroidx/core/view/MenuItemCompat$OnActionExpandListener;)Landroid/view/MenuItem;

    goto :goto_0

    .line 33
    :cond_0
    check-cast p1, Landroidx/core/view/MenuItemCompat$OnActionExpandListener;

    invoke-static {p0, p1}, Landroidx/core/view/MenuItemCompat;->setOnActionExpandListener(Landroid/view/MenuItem;Landroidx/core/view/MenuItemCompat$OnActionExpandListener;)Landroid/view/MenuItem;

    :goto_0
    return-void
.end method
