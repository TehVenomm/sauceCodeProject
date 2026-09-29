.class public final Lcom/helpshift/support/Faq;
.super Ljava/lang/Object;
.source "Faq.java"

# interfaces
.implements Landroid/os/Parcelable;


# static fields
.field public static final CREATOR:Landroid/os/Parcelable$Creator;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Landroid/os/Parcelable$Creator<",
            "Lcom/helpshift/support/Faq;",
            ">;"
        }
    .end annotation
.end field


# instance fields
.field public final body:Ljava/lang/String;

.field private categoryTags:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation
.end field

.field public id:J

.field public final is_helpful:I

.field public final is_rtl:Ljava/lang/Boolean;

.field public final language:Ljava/lang/String;

.field public final publish_id:Ljava/lang/String;

.field private qId:Ljava/lang/String;

.field public searchTerms:Ljava/util/ArrayList;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/ArrayList<",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation
.end field

.field public final section_publish_id:Ljava/lang/String;

.field private tags:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation
.end field

.field public final title:Ljava/lang/String;

.field private type:Ljava/lang/String;


# direct methods
.method static constructor <clinit>()V
    .locals 1

    .line 14
    new-instance v0, Lcom/helpshift/support/Faq$1;

    invoke-direct {v0}, Lcom/helpshift/support/Faq$1;-><init>()V

    sput-object v0, Lcom/helpshift/support/Faq;->CREATOR:Landroid/os/Parcelable$Creator;

    return-void
.end method

.method public constructor <init>(JLjava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;ILjava/lang/Boolean;Ljava/util/List;Ljava/util/List;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(J",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "I",
            "Ljava/lang/Boolean;",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;)V"
        }
    .end annotation

    .line 51
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 52
    iput-wide p1, p0, Lcom/helpshift/support/Faq;->id:J

    .line 53
    iput-object p3, p0, Lcom/helpshift/support/Faq;->qId:Ljava/lang/String;

    .line 54
    iput-object p7, p0, Lcom/helpshift/support/Faq;->title:Ljava/lang/String;

    .line 55
    iput-object p4, p0, Lcom/helpshift/support/Faq;->publish_id:Ljava/lang/String;

    .line 56
    iput-object p5, p0, Lcom/helpshift/support/Faq;->language:Ljava/lang/String;

    const-string p1, "faq"

    .line 57
    iput-object p1, p0, Lcom/helpshift/support/Faq;->type:Ljava/lang/String;

    .line 58
    iput-object p6, p0, Lcom/helpshift/support/Faq;->section_publish_id:Ljava/lang/String;

    .line 59
    iput-object p8, p0, Lcom/helpshift/support/Faq;->body:Ljava/lang/String;

    .line 60
    iput p9, p0, Lcom/helpshift/support/Faq;->is_helpful:I

    .line 61
    iput-object p10, p0, Lcom/helpshift/support/Faq;->is_rtl:Ljava/lang/Boolean;

    .line 62
    iput-object p11, p0, Lcom/helpshift/support/Faq;->tags:Ljava/util/List;

    .line 63
    iput-object p12, p0, Lcom/helpshift/support/Faq;->categoryTags:Ljava/util/List;

    return-void
.end method

.method constructor <init>(Landroid/os/Parcel;)V
    .locals 1

    .line 80
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 81
    invoke-virtual {p1}, Landroid/os/Parcel;->readString()Ljava/lang/String;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/support/Faq;->qId:Ljava/lang/String;

    .line 82
    invoke-virtual {p1}, Landroid/os/Parcel;->readString()Ljava/lang/String;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/support/Faq;->title:Ljava/lang/String;

    .line 83
    invoke-virtual {p1}, Landroid/os/Parcel;->readString()Ljava/lang/String;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/support/Faq;->publish_id:Ljava/lang/String;

    .line 84
    invoke-virtual {p1}, Landroid/os/Parcel;->readString()Ljava/lang/String;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/support/Faq;->language:Ljava/lang/String;

    .line 85
    invoke-virtual {p1}, Landroid/os/Parcel;->readString()Ljava/lang/String;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/support/Faq;->type:Ljava/lang/String;

    .line 86
    invoke-virtual {p1}, Landroid/os/Parcel;->readString()Ljava/lang/String;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/support/Faq;->section_publish_id:Ljava/lang/String;

    .line 87
    invoke-virtual {p1}, Landroid/os/Parcel;->readString()Ljava/lang/String;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/support/Faq;->body:Ljava/lang/String;

    .line 88
    invoke-virtual {p1}, Landroid/os/Parcel;->readInt()I

    move-result v0

    iput v0, p0, Lcom/helpshift/support/Faq;->is_helpful:I

    .line 89
    invoke-virtual {p1}, Landroid/os/Parcel;->readByte()B

    move-result v0

    if-eqz v0, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    invoke-static {v0}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/support/Faq;->is_rtl:Ljava/lang/Boolean;

    .line 91
    iget-object v0, p0, Lcom/helpshift/support/Faq;->searchTerms:Ljava/util/ArrayList;

    if-nez v0, :cond_1

    .line 92
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lcom/helpshift/support/Faq;->searchTerms:Ljava/util/ArrayList;

    .line 95
    :cond_1
    iget-object v0, p0, Lcom/helpshift/support/Faq;->tags:Ljava/util/List;

    if-nez v0, :cond_2

    .line 96
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lcom/helpshift/support/Faq;->tags:Ljava/util/List;

    .line 99
    :cond_2
    iget-object v0, p0, Lcom/helpshift/support/Faq;->categoryTags:Ljava/util/List;

    if-nez v0, :cond_3

    .line 100
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lcom/helpshift/support/Faq;->categoryTags:Ljava/util/List;

    .line 103
    :cond_3
    iget-object v0, p0, Lcom/helpshift/support/Faq;->searchTerms:Ljava/util/ArrayList;

    invoke-virtual {p1, v0}, Landroid/os/Parcel;->readStringList(Ljava/util/List;)V

    .line 104
    iget-object v0, p0, Lcom/helpshift/support/Faq;->tags:Ljava/util/List;

    invoke-virtual {p1, v0}, Landroid/os/Parcel;->readStringList(Ljava/util/List;)V

    .line 105
    iget-object v0, p0, Lcom/helpshift/support/Faq;->categoryTags:Ljava/util/List;

    invoke-virtual {p1, v0}, Landroid/os/Parcel;->readStringList(Ljava/util/List;)V

    return-void
.end method

.method public constructor <init>(Lcom/helpshift/faq/FaqCore;Ljava/lang/String;)V
    .locals 2

    .line 66
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const-wide/16 v0, 0x0

    .line 67
    iput-wide v0, p0, Lcom/helpshift/support/Faq;->id:J

    .line 68
    iget-object v0, p1, Lcom/helpshift/faq/FaqCore;->qId:Ljava/lang/String;

    iput-object v0, p0, Lcom/helpshift/support/Faq;->qId:Ljava/lang/String;

    .line 69
    iget-object v0, p1, Lcom/helpshift/faq/FaqCore;->publish_id:Ljava/lang/String;

    iput-object v0, p0, Lcom/helpshift/support/Faq;->publish_id:Ljava/lang/String;

    .line 70
    iget-object v0, p1, Lcom/helpshift/faq/FaqCore;->language:Ljava/lang/String;

    iput-object v0, p0, Lcom/helpshift/support/Faq;->language:Ljava/lang/String;

    .line 71
    iput-object p2, p0, Lcom/helpshift/support/Faq;->section_publish_id:Ljava/lang/String;

    .line 72
    iget-object p2, p1, Lcom/helpshift/faq/FaqCore;->title:Ljava/lang/String;

    iput-object p2, p0, Lcom/helpshift/support/Faq;->title:Ljava/lang/String;

    .line 73
    iget-object p2, p1, Lcom/helpshift/faq/FaqCore;->body:Ljava/lang/String;

    iput-object p2, p0, Lcom/helpshift/support/Faq;->body:Ljava/lang/String;

    .line 74
    iget p2, p1, Lcom/helpshift/faq/FaqCore;->is_helpful:I

    iput p2, p0, Lcom/helpshift/support/Faq;->is_helpful:I

    .line 75
    iget-object p2, p1, Lcom/helpshift/faq/FaqCore;->is_rtl:Ljava/lang/Boolean;

    iput-object p2, p0, Lcom/helpshift/support/Faq;->is_rtl:Ljava/lang/Boolean;

    .line 76
    iget-object p2, p1, Lcom/helpshift/faq/FaqCore;->tags:Ljava/util/List;

    iput-object p2, p0, Lcom/helpshift/support/Faq;->tags:Ljava/util/List;

    .line 77
    iget-object p1, p1, Lcom/helpshift/faq/FaqCore;->categoryTags:Ljava/util/List;

    iput-object p1, p0, Lcom/helpshift/support/Faq;->categoryTags:Ljava/util/List;

    return-void
.end method

.method private static mergeSearchTerms(Ljava/util/ArrayList;Ljava/util/ArrayList;)Ljava/util/ArrayList;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/ArrayList<",
            "Ljava/lang/String;",
            ">;",
            "Ljava/util/ArrayList<",
            "Ljava/lang/String;",
            ">;)",
            "Ljava/util/ArrayList<",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation

    .line 110
    new-instance v0, Ljava/util/HashSet;

    invoke-direct {v0}, Ljava/util/HashSet;-><init>()V

    if-eqz p0, :cond_0

    .line 112
    invoke-virtual {v0, p0}, Ljava/util/HashSet;->addAll(Ljava/util/Collection;)Z

    :cond_0
    if-eqz p1, :cond_1

    .line 115
    invoke-virtual {v0, p1}, Ljava/util/HashSet;->addAll(Ljava/util/Collection;)Z

    .line 117
    :cond_1
    new-instance p0, Ljava/util/ArrayList;

    invoke-direct {p0, v0}, Ljava/util/ArrayList;-><init>(Ljava/util/Collection;)V

    return-object p0
.end method


# virtual methods
.method protected addSearchTerms(Ljava/util/ArrayList;)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/ArrayList<",
            "Ljava/lang/String;",
            ">;)V"
        }
    .end annotation

    .line 168
    iget-object v0, p0, Lcom/helpshift/support/Faq;->searchTerms:Ljava/util/ArrayList;

    invoke-static {v0, p1}, Lcom/helpshift/support/Faq;->mergeSearchTerms(Ljava/util/ArrayList;Ljava/util/ArrayList;)Ljava/util/ArrayList;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/support/Faq;->searchTerms:Ljava/util/ArrayList;

    return-void
.end method

.method protected clearSearchTerms()V
    .locals 1

    const/4 v0, 0x0

    .line 164
    iput-object v0, p0, Lcom/helpshift/support/Faq;->searchTerms:Ljava/util/ArrayList;

    return-void
.end method

.method public describeContents()I
    .locals 1

    const/4 v0, 0x0

    return v0
.end method

.method public equals(Ljava/lang/Object;)Z
    .locals 3

    .line 173
    check-cast p1, Lcom/helpshift/support/Faq;

    const/4 v0, 0x0

    if-nez p1, :cond_0

    return v0

    .line 179
    :cond_0
    iget-object v1, p0, Lcom/helpshift/support/Faq;->qId:Ljava/lang/String;

    iget-object v2, p1, Lcom/helpshift/support/Faq;->qId:Ljava/lang/String;

    invoke-virtual {v1, v2}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_1

    iget-object v1, p0, Lcom/helpshift/support/Faq;->title:Ljava/lang/String;

    iget-object v2, p1, Lcom/helpshift/support/Faq;->title:Ljava/lang/String;

    .line 180
    invoke-virtual {v1, v2}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_1

    iget-object v1, p0, Lcom/helpshift/support/Faq;->body:Ljava/lang/String;

    iget-object v2, p1, Lcom/helpshift/support/Faq;->body:Ljava/lang/String;

    .line 181
    invoke-virtual {v1, v2}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_1

    iget-object v1, p0, Lcom/helpshift/support/Faq;->publish_id:Ljava/lang/String;

    iget-object v2, p1, Lcom/helpshift/support/Faq;->publish_id:Ljava/lang/String;

    .line 182
    invoke-virtual {v1, v2}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_1

    iget-object v1, p0, Lcom/helpshift/support/Faq;->language:Ljava/lang/String;

    iget-object v2, p1, Lcom/helpshift/support/Faq;->language:Ljava/lang/String;

    .line 183
    invoke-virtual {v1, v2}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_1

    iget-object v1, p0, Lcom/helpshift/support/Faq;->section_publish_id:Ljava/lang/String;

    iget-object v2, p1, Lcom/helpshift/support/Faq;->section_publish_id:Ljava/lang/String;

    .line 184
    invoke-virtual {v1, v2}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_1

    iget-object v1, p0, Lcom/helpshift/support/Faq;->is_rtl:Ljava/lang/Boolean;

    iget-object v2, p1, Lcom/helpshift/support/Faq;->is_rtl:Ljava/lang/Boolean;

    if-ne v1, v2, :cond_1

    iget v1, p0, Lcom/helpshift/support/Faq;->is_helpful:I

    iget v2, p1, Lcom/helpshift/support/Faq;->is_helpful:I

    if-ne v1, v2, :cond_1

    iget-object v1, p0, Lcom/helpshift/support/Faq;->tags:Ljava/util/List;

    iget-object v2, p1, Lcom/helpshift/support/Faq;->tags:Ljava/util/List;

    .line 187
    invoke-interface {v1, v2}, Ljava/util/List;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_1

    iget-object v1, p0, Lcom/helpshift/support/Faq;->categoryTags:Ljava/util/List;

    iget-object p1, p1, Lcom/helpshift/support/Faq;->categoryTags:Ljava/util/List;

    .line 188
    invoke-interface {v1, p1}, Ljava/util/List;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_1

    const/4 v0, 0x1

    :cond_1
    return v0
.end method

.method public getCategoryTags()Ljava/util/List;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation

    .line 155
    iget-object v0, p0, Lcom/helpshift/support/Faq;->categoryTags:Ljava/util/List;

    if-nez v0, :cond_0

    .line 156
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    return-object v0

    .line 159
    :cond_0
    iget-object v0, p0, Lcom/helpshift/support/Faq;->categoryTags:Ljava/util/List;

    return-object v0
.end method

.method public getId()Ljava/lang/String;
    .locals 1

    .line 142
    iget-object v0, p0, Lcom/helpshift/support/Faq;->qId:Ljava/lang/String;

    return-object v0
.end method

.method public getTags()Ljava/util/List;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation

    .line 146
    iget-object v0, p0, Lcom/helpshift/support/Faq;->tags:Ljava/util/List;

    if-nez v0, :cond_0

    .line 147
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    return-object v0

    .line 150
    :cond_0
    iget-object v0, p0, Lcom/helpshift/support/Faq;->tags:Ljava/util/List;

    return-object v0
.end method

.method public toString()Ljava/lang/String;
    .locals 1

    .line 193
    iget-object v0, p0, Lcom/helpshift/support/Faq;->title:Ljava/lang/String;

    return-object v0
.end method

.method public writeToParcel(Landroid/os/Parcel;I)V
    .locals 0

    .line 127
    iget-object p2, p0, Lcom/helpshift/support/Faq;->qId:Ljava/lang/String;

    invoke-virtual {p1, p2}, Landroid/os/Parcel;->writeString(Ljava/lang/String;)V

    .line 128
    iget-object p2, p0, Lcom/helpshift/support/Faq;->title:Ljava/lang/String;

    invoke-virtual {p1, p2}, Landroid/os/Parcel;->writeString(Ljava/lang/String;)V

    .line 129
    iget-object p2, p0, Lcom/helpshift/support/Faq;->publish_id:Ljava/lang/String;

    invoke-virtual {p1, p2}, Landroid/os/Parcel;->writeString(Ljava/lang/String;)V

    .line 130
    iget-object p2, p0, Lcom/helpshift/support/Faq;->language:Ljava/lang/String;

    invoke-virtual {p1, p2}, Landroid/os/Parcel;->writeString(Ljava/lang/String;)V

    .line 131
    iget-object p2, p0, Lcom/helpshift/support/Faq;->type:Ljava/lang/String;

    invoke-virtual {p1, p2}, Landroid/os/Parcel;->writeString(Ljava/lang/String;)V

    .line 132
    iget-object p2, p0, Lcom/helpshift/support/Faq;->section_publish_id:Ljava/lang/String;

    invoke-virtual {p1, p2}, Landroid/os/Parcel;->writeString(Ljava/lang/String;)V

    .line 133
    iget-object p2, p0, Lcom/helpshift/support/Faq;->body:Ljava/lang/String;

    invoke-virtual {p1, p2}, Landroid/os/Parcel;->writeString(Ljava/lang/String;)V

    .line 134
    iget p2, p0, Lcom/helpshift/support/Faq;->is_helpful:I

    invoke-virtual {p1, p2}, Landroid/os/Parcel;->writeInt(I)V

    .line 135
    iget-object p2, p0, Lcom/helpshift/support/Faq;->is_rtl:Ljava/lang/Boolean;

    invoke-virtual {p2}, Ljava/lang/Boolean;->booleanValue()Z

    move-result p2

    int-to-byte p2, p2

    invoke-virtual {p1, p2}, Landroid/os/Parcel;->writeByte(B)V

    .line 136
    iget-object p2, p0, Lcom/helpshift/support/Faq;->searchTerms:Ljava/util/ArrayList;

    invoke-virtual {p1, p2}, Landroid/os/Parcel;->writeStringList(Ljava/util/List;)V

    .line 137
    iget-object p2, p0, Lcom/helpshift/support/Faq;->tags:Ljava/util/List;

    invoke-virtual {p1, p2}, Landroid/os/Parcel;->writeStringList(Ljava/util/List;)V

    .line 138
    iget-object p2, p0, Lcom/helpshift/support/Faq;->categoryTags:Ljava/util/List;

    invoke-virtual {p1, p2}, Landroid/os/Parcel;->writeStringList(Ljava/util/List;)V

    return-void
.end method
