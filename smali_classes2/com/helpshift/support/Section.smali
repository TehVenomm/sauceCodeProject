.class public Lcom/helpshift/support/Section;
.super Ljava/lang/Object;
.source "Section.java"

# interfaces
.implements Landroid/os/Parcelable;


# static fields
.field public static final CREATOR:Landroid/os/Parcelable$Creator;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Landroid/os/Parcelable$Creator<",
            "Lcom/helpshift/support/Section;",
            ">;"
        }
    .end annotation
.end field


# instance fields
.field private id:J

.field private publish_id:Ljava/lang/String;

.field private section_id:Ljava/lang/String;

.field private title:Ljava/lang/String;


# direct methods
.method static constructor <clinit>()V
    .locals 1

    .line 7
    new-instance v0, Lcom/helpshift/support/Section$1;

    invoke-direct {v0}, Lcom/helpshift/support/Section$1;-><init>()V

    sput-object v0, Lcom/helpshift/support/Section;->CREATOR:Landroid/os/Parcelable$Creator;

    return-void
.end method

.method public constructor <init>()V
    .locals 2

    .line 25
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const-wide/16 v0, -0x1

    .line 26
    iput-wide v0, p0, Lcom/helpshift/support/Section;->id:J

    const-string v0, ""

    .line 27
    iput-object v0, p0, Lcom/helpshift/support/Section;->section_id:Ljava/lang/String;

    const-string v0, ""

    .line 28
    iput-object v0, p0, Lcom/helpshift/support/Section;->publish_id:Ljava/lang/String;

    const-string v0, ""

    .line 29
    iput-object v0, p0, Lcom/helpshift/support/Section;->title:Ljava/lang/String;

    return-void
.end method

.method public constructor <init>(JLjava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    .locals 0

    .line 33
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 34
    iput-wide p1, p0, Lcom/helpshift/support/Section;->id:J

    .line 35
    iput-object p3, p0, Lcom/helpshift/support/Section;->section_id:Ljava/lang/String;

    .line 36
    iput-object p4, p0, Lcom/helpshift/support/Section;->title:Ljava/lang/String;

    .line 37
    iput-object p5, p0, Lcom/helpshift/support/Section;->publish_id:Ljava/lang/String;

    return-void
.end method

.method constructor <init>(Landroid/os/Parcel;)V
    .locals 1

    .line 40
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 41
    invoke-virtual {p1}, Landroid/os/Parcel;->readString()Ljava/lang/String;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/support/Section;->section_id:Ljava/lang/String;

    .line 42
    invoke-virtual {p1}, Landroid/os/Parcel;->readString()Ljava/lang/String;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/support/Section;->title:Ljava/lang/String;

    .line 43
    invoke-virtual {p1}, Landroid/os/Parcel;->readString()Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/support/Section;->publish_id:Ljava/lang/String;

    return-void
.end method


# virtual methods
.method public describeContents()I
    .locals 1

    const/4 v0, 0x0

    return v0
.end method

.method public equals(Ljava/lang/Object;)Z
    .locals 3

    .line 60
    check-cast p1, Lcom/helpshift/support/Section;

    const/4 v0, 0x0

    if-nez p1, :cond_0

    return v0

    .line 66
    :cond_0
    iget-object v1, p0, Lcom/helpshift/support/Section;->title:Ljava/lang/String;

    iget-object v2, p1, Lcom/helpshift/support/Section;->title:Ljava/lang/String;

    invoke-virtual {v1, v2}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_1

    iget-object v1, p0, Lcom/helpshift/support/Section;->publish_id:Ljava/lang/String;

    iget-object v2, p1, Lcom/helpshift/support/Section;->publish_id:Ljava/lang/String;

    .line 67
    invoke-virtual {v1, v2}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_1

    iget-object v1, p0, Lcom/helpshift/support/Section;->section_id:Ljava/lang/String;

    iget-object p1, p1, Lcom/helpshift/support/Section;->section_id:Ljava/lang/String;

    .line 68
    invoke-virtual {v1, p1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_1

    const/4 v0, 0x1

    :cond_1
    return v0
.end method

.method public getPublishId()Ljava/lang/String;
    .locals 1

    .line 47
    iget-object v0, p0, Lcom/helpshift/support/Section;->publish_id:Ljava/lang/String;

    return-object v0
.end method

.method public getSectionId()Ljava/lang/String;
    .locals 1

    .line 55
    iget-object v0, p0, Lcom/helpshift/support/Section;->section_id:Ljava/lang/String;

    return-object v0
.end method

.method public getTitle()Ljava/lang/String;
    .locals 1

    .line 51
    iget-object v0, p0, Lcom/helpshift/support/Section;->title:Ljava/lang/String;

    return-object v0
.end method

.method public toString()Ljava/lang/String;
    .locals 1

    .line 72
    iget-object v0, p0, Lcom/helpshift/support/Section;->title:Ljava/lang/String;

    return-object v0
.end method

.method public writeToParcel(Landroid/os/Parcel;I)V
    .locals 0

    .line 82
    iget-object p2, p0, Lcom/helpshift/support/Section;->section_id:Ljava/lang/String;

    invoke-virtual {p1, p2}, Landroid/os/Parcel;->writeString(Ljava/lang/String;)V

    .line 83
    iget-object p2, p0, Lcom/helpshift/support/Section;->title:Ljava/lang/String;

    invoke-virtual {p1, p2}, Landroid/os/Parcel;->writeString(Ljava/lang/String;)V

    .line 84
    iget-object p2, p0, Lcom/helpshift/support/Section;->publish_id:Ljava/lang/String;

    invoke-virtual {p1, p2}, Landroid/os/Parcel;->writeString(Ljava/lang/String;)V

    return-void
.end method
