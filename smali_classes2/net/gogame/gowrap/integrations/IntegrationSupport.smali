.class public interface abstract Lnet/gogame/gowrap/integrations/IntegrationSupport;
.super Ljava/lang/Object;
.source "IntegrationSupport.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lnet/gogame/gowrap/integrations/IntegrationSupport$IntegrationContext;
    }
.end annotation


# virtual methods
.method public abstract getId()Ljava/lang/String;
.end method

.method public abstract init(Landroid/app/Activity;Lnet/gogame/gowrap/integrations/Config;Lnet/gogame/gowrap/integrations/IntegrationSupport$IntegrationContext;)V
.end method

.method public abstract isIntegrated()Z
.end method

.method public abstract onActivityCreated(Landroid/app/Activity;)V
.end method

.method public abstract onActivityDestroyed(Landroid/app/Activity;)V
.end method

.method public abstract onActivityPaused(Landroid/app/Activity;)V
.end method

.method public abstract onActivityResumed(Landroid/app/Activity;)V
.end method

.method public abstract onActivityStarted(Landroid/app/Activity;)V
.end method

.method public abstract onActivityStopped(Landroid/app/Activity;)V
.end method
